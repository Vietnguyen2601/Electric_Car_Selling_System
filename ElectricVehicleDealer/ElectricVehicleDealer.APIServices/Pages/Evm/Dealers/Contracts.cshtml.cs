using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Dealers;

public class OperationsModel : PageModel
{
    private readonly IStationService _stationService;
    private readonly IStationCarService _stationCarService;

    private const int LowInventoryThreshold = 200;

    public OperationsModel(IStationService stationService, IStationCarService stationCarService)
    {
        _stationService = stationService;
        _stationCarService = stationCarService;
    }

    public IReadOnlyList<StationOperationsViewModel> StationOverview { get; private set; } =
        Array.Empty<StationOperationsViewModel>();

    public IReadOnlyList<OperationsAlertViewModel> Alerts { get; private set; } =
        Array.Empty<OperationsAlertViewModel>();

    public IReadOnlyList<StationUpdateSummary> RecentUpdates { get; private set; } =
        Array.Empty<StationUpdateSummary>();

    public int TotalStations { get; private set; }
    public int ActiveStations { get; private set; }
    public int StationsWithLowInventory { get; private set; }
    public int TotalVehiclesReady { get; private set; }

    public async Task OnGetAsync()
    {
        var stations = (await _stationService.GetAllAsync()).ToList();
        var assignments = (await _stationCarService.GetAllAsync()).ToList();

        var overview = BuildStationOverview(stations, assignments);
        StationOverview = overview;

        TotalStations = stations.Count;
        ActiveStations = stations.Count(s => s.IsActive);
        TotalVehiclesReady = overview.Sum(o => o.ActiveVehicles);
        StationsWithLowInventory = overview.Count(o => o.HasLowInventory);

        Alerts = BuildAlerts(overview);
        RecentUpdates = BuildRecentUpdates(stations, overview);
    }

    private static List<StationOperationsViewModel> BuildStationOverview(
        IReadOnlyCollection<StationDTO> stations,
        IReadOnlyCollection<StationCarDTO> assignments)
    {
        var assignmentLookup = assignments
            .GroupBy(a => a.StationId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var result = new List<StationOperationsViewModel>(stations.Count);

        foreach (var station in stations.OrderBy(s => s.StationName))
        {
            assignmentLookup.TryGetValue(station.StationId, out var stationAssignments);

            var totalVehicles = stationAssignments?.Sum(a => a.Quantity) ?? 0;
            var activeVehicles = stationAssignments?.Where(a => a.IsActive).Sum(a => a.Quantity) ?? 0;
            var distinctModels = stationAssignments == null
                ? 0
                : stationAssignments
                    .Select(a => NormalizeVehicleName(a))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Count();
            var hasLowInventory = station.IsActive && activeVehicles > 0 && activeVehicles < LowInventoryThreshold;
            var hasInventory = activeVehicles > 0;

            result.Add(new StationOperationsViewModel(
                station.StationId,
                station.StationName,
                station.Location,
                station.ContactNumber,
                station.IsActive,
                totalVehicles,
                activeVehicles,
                distinctModels,
                hasLowInventory,
                hasInventory));
        }

        return result;
    }

    private static IReadOnlyList<OperationsAlertViewModel> BuildAlerts(
        IEnumerable<StationOperationsViewModel> overview)
    {
        var alerts = new List<OperationsAlertViewModel>();

        foreach (var station in overview)
        {
            if (!station.IsActive)
            {
                alerts.Add(new OperationsAlertViewModel(
                    "danger",
                    station.StationName,
                    "Station đang tạm dừng bán hàng. Cần xác nhận kế hoạch mở lại."));
            }

            if (!station.HasInventory)
            {
                alerts.Add(new OperationsAlertViewModel(
                    "info",
                    station.StationName,
                    "Chưa có đội xe trưng bày tại station này. Hãy điều phối xe sớm."));
            }
            else if (station.HasLowInventory)
            {
                alerts.Add(new OperationsAlertViewModel(
                    "warning",
                    station.StationName,
                    $"Tồn kho hoạt động chỉ còn {station.ActiveVehicles} xe (< {LowInventoryThreshold}). Cần bổ sung xe trưng bày."));
            }
        }

        return alerts.Take(6).ToList();
    }

    private static IReadOnlyList<StationUpdateSummary> BuildRecentUpdates(
        IEnumerable<StationDTO> stations,
        IEnumerable<StationOperationsViewModel> overview)
    {
        var overviewById = overview.ToDictionary(o => o.StationId);

        return stations
            .OrderByDescending(s => s.UpdatedAt ?? s.CreatedAt)
            .Take(4)
            .Select(station =>
            {
                var key = station.StationId;
                overviewById.TryGetValue(key, out var stats);

                var timestamp = station.UpdatedAt ?? station.CreatedAt;
                var message = stats switch
                {
                    null => "Chưa có dữ liệu đội xe.",
                    _ when !stats.IsActive => "Station tạm ngưng phục vụ khách.",
                    _ when stats.ActiveVehicles == 0 => "Không còn xe trưng bày sẵn sàng.",
                    _ => $"{stats.ActiveVehicles} xe sẵn sàng với {stats.DistinctModels} mẫu đang mở bán."
                };

                return new StationUpdateSummary(
                    station.StationName,
                    timestamp,
                    message);
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

    public record StationOperationsViewModel(
        int StationId,
        string StationName,
        string Location,
        string? ContactNumber,
        bool IsActive,
        int TotalVehicles,
        int ActiveVehicles,
        int DistinctModels,
        bool HasLowInventory,
        bool HasInventory);

    public record OperationsAlertViewModel(
        string Severity,
        string StationName,
        string Message);

    public record StationUpdateSummary(
        string StationName,
        DateTime Timestamp,
        string Message);
}
