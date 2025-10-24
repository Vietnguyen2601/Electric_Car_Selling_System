using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Dealers;

public class FleetModel : PageModel
{
    private readonly IStationService _stationService;
    private readonly IStationCarService _stationCarService;

    private const int StationLowInventoryThreshold = 200;
    private const int ModelLowInventoryThreshold = 5;

    public FleetModel(IStationService stationService, IStationCarService stationCarService)
    {
        _stationService = stationService;
        _stationCarService = stationCarService;
    }

    public IReadOnlyList<StationInventoryViewModel> StationInventories { get; private set; } =
        Array.Empty<StationInventoryViewModel>();

    public IReadOnlyList<VehicleSummaryViewModel> VehicleSummaries { get; private set; } =
        Array.Empty<VehicleSummaryViewModel>();

    public IReadOnlyList<FleetFocusNote> FocusNotes { get; private set; } =
        Array.Empty<FleetFocusNote>();

    public int TotalVehicles { get; private set; }
    public int ActiveAssignments { get; private set; }
    public int StationsWithInventory { get; private set; }
    public int LowStockAssignments { get; private set; }

    public async Task OnGetAsync()
    {
        var stations = (await _stationService.GetAllAsync()).ToList();
        var assignments = (await _stationCarService.GetAllAsync()).ToList();

        StationInventories = BuildStationInventories(stations, assignments);
        VehicleSummaries = BuildVehicleSummaries(assignments);
        FocusNotes = BuildFocusNotes(StationInventories);

        TotalVehicles = assignments.Sum(a => a.Quantity);
        ActiveAssignments = assignments.Count(a => a.IsActive);
        StationsWithInventory = StationInventories.Count(i => i.TotalQuantity > 0);
        LowStockAssignments = assignments.Count(a => a.IsActive && a.Quantity <= ModelLowInventoryThreshold);
    }

    private static IReadOnlyList<StationInventoryViewModel> BuildStationInventories(
        IReadOnlyCollection<StationDTO> stations,
        IReadOnlyCollection<StationCarDTO> assignments)
    {
        var assignmentLookup = assignments
            .GroupBy(a => a.StationId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var results = new List<StationInventoryViewModel>(stations.Count);

        foreach (var station in stations.OrderBy(s => s.StationName))
        {
            assignmentLookup.TryGetValue(station.StationId, out var stationAssignments);

            var vehicles = stationAssignments?
                .OrderByDescending(a => a.Quantity)
                .Select(a => new VehicleAllocationViewModel(
                    NormalizeVehicleName(a),
                    a.Quantity,
                    a.IsActive))
                .ToList() ?? new List<VehicleAllocationViewModel>();

            var totalQuantity = vehicles.Sum(v => v.Quantity);
            var activeQuantity = stationAssignments?.Where(a => a.IsActive).Sum(a => a.Quantity) ?? 0;
            var suspendedQuantity = totalQuantity - activeQuantity;
            var hasLowStock = station.IsActive && activeQuantity > 0 && activeQuantity < StationLowInventoryThreshold;

            results.Add(new StationInventoryViewModel(
                station.StationId,
                station.StationName,
                station.Location,
                station.IsActive,
                vehicles.Count,
                vehicles.Count(v => v.IsActive),
                totalQuantity,
                activeQuantity,
                suspendedQuantity,
                hasLowStock,
                vehicles));
        }

        return results;
    }

    private static IReadOnlyList<VehicleSummaryViewModel> BuildVehicleSummaries(
        IReadOnlyCollection<StationCarDTO> assignments)
    {
        return assignments
            .GroupBy(a => NormalizeVehicleName(a))
            .Select(group =>
            {
                var totalQuantity = group.Sum(x => x.Quantity);
                var activeStations = group.Where(x => x.IsActive).Select(x => x.StationId).Distinct().Count();
                var lowStockStations = group
                    .Where(x => x.IsActive && x.Quantity <= ModelLowInventoryThreshold)
                    .Select(x => x.StationId)
                    .Distinct()
                    .Count();
                var averagePerStation = group.Any()
                    ? (int)Math.Round(group.Average(x => x.Quantity), MidpointRounding.AwayFromZero)
                    : 0;

                return new VehicleSummaryViewModel(
                    group.Key,
                    totalQuantity,
                    activeStations,
                    lowStockStations,
                    averagePerStation);
            })
            .OrderByDescending(summary => summary.TotalQuantity)
            .ToList();
    }

    private static IReadOnlyList<FleetFocusNote> BuildFocusNotes(
        IEnumerable<StationInventoryViewModel> inventories)
    {
        return inventories
            .OrderBy(inv => inv.HasLowStock ? 0 : 1)
            .ThenBy(inv => inv.ActiveQuantity)
            .Take(4)
            .Select(inv =>
            {
                var message = inv.HasLowStock
                    ? $"Tồn kho thấp với {inv.ActiveQuantity} xe hoạt động. Cần điều phối thêm."
                    : $"Đang trưng bày {inv.ActiveQuantity} xe với {inv.ActiveModels} mẫu.";

                if (!inv.IsActive)
                {
                    message += " Station đang tạm ngưng đón khách.";
                }

                return new FleetFocusNote(inv.StationName, message);
            })
            .ToList();
    }

    private static string NormalizeVehicleName(StationCarDTO dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.VehicleModel))
        {
            return dto.VehicleModel.Trim();
        }

        return $"Model #{dto.VehicleId}";
    }

    public record StationInventoryViewModel(
        int StationId,
        string StationName,
        string Location,
        bool IsActive,
        int TotalModels,
        int ActiveModels,
        int TotalQuantity,
        int ActiveQuantity,
        int SuspendedQuantity,
        bool HasLowStock,
        IReadOnlyList<VehicleAllocationViewModel> Vehicles);

    public record VehicleAllocationViewModel(
        string VehicleName,
        int Quantity,
        bool IsActive);

    public record VehicleSummaryViewModel(
        string VehicleName,
        int TotalQuantity,
        int ActiveStations,
        int LowStockStations,
        int AveragePerStation);

    public record FleetFocusNote(
        string StationName,
        string Message);
}
