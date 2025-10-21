using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Dealers;

public class AccessModel : PageModel
{
    private readonly IStationService _stationService;
    private readonly IStationCarService _stationCarService;

    public AccessModel(IStationService stationService, IStationCarService stationCarService)
    {
        _stationService = stationService;
        _stationCarService = stationCarService;
    }

    public IReadOnlyList<StationAccessItem> Stations { get; private set; } = Array.Empty<StationAccessItem>();
    public int ActiveStations { get; private set; }
    public int InactiveStations { get; private set; }
    public int RecentlyUpdatedStations { get; private set; }

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task OnGetAsync()
    {
        await LoadAsync();
    }

    public async Task<IActionResult> OnPostToggleStatusAsync(int stationId)
    {
        if (stationId <= 0)
        {
            StatusMessage = "Không tìm thấy station để cập nhật.";
            return RedirectToPage();
        }

        var station = await _stationService.GetByIdAsync(stationId);
        if (station == null)
        {
            StatusMessage = "Station không tồn tại.";
            return RedirectToPage();
        }

        station.IsActive = !station.IsActive;
        var updated = await _stationService.UpdateAsync(stationId, station);

        StatusMessage = updated
            ? $"Đã {(station.IsActive ? "mở lại" : "tạm ngưng")} station {station.StationName}."
            : "Không thể cập nhật trạng thái station.";

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateContactAsync(int stationId, string? contactNumber)
    {
        if (stationId <= 0)
        {
            StatusMessage = "Không tìm thấy station để cập nhật.";
            return RedirectToPage();
        }

        var station = await _stationService.GetByIdAsync(stationId);
        if (station == null)
        {
            StatusMessage = "Station không tồn tại.";
            return RedirectToPage();
        }

        station.ContactNumber = string.IsNullOrWhiteSpace(contactNumber)
            ? null
            : contactNumber.Trim();

        var updated = await _stationService.UpdateAsync(stationId, station);
        StatusMessage = updated
            ? $"Đã cập nhật liên hệ cho {station.StationName}."
            : "Không thể cập nhật thông tin liên hệ.";

        return RedirectToPage();
    }

    private async Task LoadAsync()
    {
        var stations = (await _stationService.GetAllAsync()).ToList();
        var assignments = (await _stationCarService.GetAllAsync()).ToList();

        var assignmentLookup = assignments
            .GroupBy(a => a.StationId)
            .ToDictionary(group => group.Key, group => group.ToList());

        var items = new List<StationAccessItem>(stations.Count);

        foreach (var station in stations.OrderByDescending(s => s.IsActive).ThenBy(s => s.StationName))
        {
            assignmentLookup.TryGetValue(station.StationId, out var stationAssignments);

            var totalVehicles = stationAssignments?.Sum(a => a.Quantity) ?? 0;
            var activeVehicles = stationAssignments?.Where(a => a.IsActive).Sum(a => a.Quantity) ?? 0;
            var modelCount = stationAssignments == null
                ? 0
                : stationAssignments
                    .Select(a => NormalizeVehicleName(a))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Count();

            items.Add(new StationAccessItem(
                station.StationId,
                station.StationName,
                station.Location,
                station.ContactNumber,
                station.IsActive,
                station.CreatedAt,
                station.UpdatedAt,
                totalVehicles,
                activeVehicles,
                modelCount));
        }

        Stations = items;
        ActiveStations = items.Count(i => i.IsActive);
        InactiveStations = items.Count - ActiveStations;

        var recentThreshold = DateTime.UtcNow.AddDays(-7);
        RecentlyUpdatedStations = items.Count(i => i.LastUpdated >= recentThreshold);
    }

    private static string NormalizeVehicleName(StationCarDTO dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.VehicleModel))
        {
            return dto.VehicleModel.Trim();
        }

        return $"Model #{dto.VehicleId}";
    }

    public record StationAccessItem(
        int StationId,
        string StationName,
        string Location,
        string? ContactNumber,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int TotalVehicles,
        int ActiveVehicles,
        int ModelCount)
    {
        public DateTime LastUpdated => UpdatedAt ?? CreatedAt;
        public int InactiveVehicles => TotalVehicles - ActiveVehicles;
    }
}
