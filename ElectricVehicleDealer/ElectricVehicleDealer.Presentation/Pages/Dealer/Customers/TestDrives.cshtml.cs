using System.Globalization;
using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.ScheduleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Dealer.Customers
{
    public class TestDrivesModel : PageModel
    {
        private readonly IScheduleService _scheduleService;

        public TestDrivesModel(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public IReadOnlyList<ScheduleDto> Schedules { get; private set; } = Array.Empty<ScheduleDto>();

        public int PendingCount => Schedules.Count(schedule => string.Equals(schedule.Status, "Pending", StringComparison.OrdinalIgnoreCase));
        public int ConfirmedCount => Schedules.Count(schedule => string.Equals(schedule.Status, "Confirmed", StringComparison.OrdinalIgnoreCase));
        public int CompletedCount => Schedules.Count(schedule => string.Equals(schedule.Status, "Completed", StringComparison.OrdinalIgnoreCase));

        [TempData]
        public string? SuccessMessage { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            await LoadSchedulesAsync();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            var normalizedStatus = NormalizeStatus(status);
            if (string.IsNullOrWhiteSpace(normalizedStatus))
            {
                ErrorMessage = "Trạng thái không hợp lệ.";
                return RedirectToPage();
            }

            var updateDto = new UpdateScheduleDto
            {
                Status = normalizedStatus
            };

            var updated = await _scheduleService.UpdateAsync(id, updateDto);
            if (!updated)
            {
                ErrorMessage = "Không thể cập nhật lịch lái thử. Vui lòng thử lại.";
                return RedirectToPage();
            }

            SuccessMessage = normalizedStatus switch
            {
                "Confirmed" => "Đã xác nhận lịch lái thử.",
                "Completed" => "Đã đánh dấu lịch lái thử hoàn tất.",
                "Cancelled" => "Đã huỷ lịch lái thử.",
                _ => "Cập nhật trạng thái thành công."
            };

            return RedirectToPage();
        }

        public string GetStatusClass(string? status)
        {
            var value = status?.Trim().ToLowerInvariant() ?? string.Empty;
            return value switch
            {
                "completed" => "crud-tag--success",
                "confirmed" => "crud-tag--success",
                "pending" => "crud-tag--warning",
                "processing" => "crud-tag--warning",
                "cancelled" or "canceled" => "crud-tag--danger",
                _ => "crud-tag--neutral"
            };
        }

        public string GetStatusLabel(string? status)
        {
            if (string.IsNullOrWhiteSpace(status)) return "Không rõ";
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(status.Trim().ToLowerInvariant());
        }

        public bool CanConfirm(string? status)
        {
            var value = status?.Trim().ToLowerInvariant();
            return value is "pending" or "processing";
        }

        public bool CanComplete(string? status)
        {
            var value = status?.Trim().ToLowerInvariant();
            return value is "confirmed" or "pending";
        }

        public bool CanCancel(string? status)
        {
            var value = status?.Trim().ToLowerInvariant();
            return value is "pending" or "confirmed" or "processing";
        }

        private async Task LoadSchedulesAsync()
        {
            var schedules = await _scheduleService.GetAllAsync();
            Schedules = schedules
                .OrderBy(schedule => schedule.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase) ? 1 : 0)
                .ThenBy(schedule => schedule.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) ? 1 : 0)
                .ThenBy(schedule => schedule.ScheduleTime)
                .ToList();
        }

        private static string? NormalizeStatus(string? status)
        {
            if (string.IsNullOrWhiteSpace(status)) return null;
            var value = status.Trim().ToLowerInvariant();
            return value switch
            {
                "pending" => "Pending",
                "confirmed" => "Confirmed",
                "completed" => "Completed",
                "cancelled" or "canceled" or "cancel" => "Cancelled",
                _ => null
            };
        }
    }
}
