using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using ElectricVehicleDealer.Common.DTOs.ScheduleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ElectricVehicleDealer.Presentation.Pages.Dashboard
{
    public class CustomerModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IScheduleService _scheduleService;

        public CustomerModel(IOrderService orderService, IScheduleService scheduleService)
        {
            _orderService = orderService;
            _scheduleService = scheduleService;
        }

        public List<OrderDto> DepositOrders { get; private set; } = new();
        public List<ScheduleDto> TestDriveRequests { get; private set; } = new();

        public int PendingDepositCount =>
            DepositOrders.Count(order => string.Equals(order.Status, "Pending", StringComparison.OrdinalIgnoreCase));

        public int ConfirmedTestDriveCount =>
            TestDriveRequests.Count(schedule => string.Equals(schedule.Status, "Confirmed", StringComparison.OrdinalIgnoreCase));

        public int AssignedDepositCount =>
            DepositOrders.Count(order => !string.IsNullOrWhiteSpace(order.StaffUsername));

        public int AwaitingAssignmentCount =>
            DepositOrders.Count(order => string.IsNullOrWhiteSpace(order.StaffUsername));

        public int PendingTestDriveCount =>
            TestDriveRequests.Count(schedule => string.Equals(schedule.Status, "Pending", StringComparison.OrdinalIgnoreCase));

        public ScheduleDto? NextConfirmedTestDrive =>
            TestDriveRequests
                .Where(schedule => string.Equals(schedule.Status, "Confirmed", StringComparison.OrdinalIgnoreCase))
                .OrderBy(schedule => schedule.ScheduleTime)
                .FirstOrDefault();

        public async Task<IActionResult> OnGetAsync()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (!accountId.HasValue)
            {
                var returnUrl = HttpContext.Request.Path.HasValue
                    ? HttpContext.Request.Path.Value
                    : "/dashboard/customer";

                return RedirectToPage("/Store/Login", new { returnUrl });
            }

            var orders = await _orderService.GetAllAsync();
            DepositOrders = orders
                .Where(order => order.CustomerId == accountId.Value)
                .OrderByDescending(order => order.CreatedAt)
                .ToList();

            var schedules = await _scheduleService.GetAllAsync();
            TestDriveRequests = schedules
                .Where(schedule => schedule.CustomerId == accountId.Value)
                .OrderByDescending(schedule => schedule.CreatedAt)
                .ToList();

            return Page();
        }

        public string GetOrderStatusClass(string? rawStatus)
        {
            var status = (rawStatus ?? string.Empty).ToLowerInvariant();
            return status switch
            {
                "pending" => "crud-tag--warning",
                "confirmed" or "completed" => "crud-tag--success",
                "cancelled" => "crud-tag--danger",
                _ => "crud-tag--neutral"
            };
        }

        public string GetOrderStatusLabel(string? rawStatus)
        {
            var status = (rawStatus ?? string.Empty).ToLowerInvariant();
            return status switch
            {
                "pending" => "Đang chờ xử lý",
                "confirmed" => "Đã xác nhận",
                "completed" => "Hoàn tất",
                "cancelled" => "Đã hủy",
                _ => "Đang cập nhật"
            };
        }

        public string GetScheduleStatusClass(string? rawStatus)
        {
            var status = (rawStatus ?? string.Empty).ToLowerInvariant();
            return status switch
            {
                "pending" => "crud-tag--warning",
                "confirmed" or "completed" => "crud-tag--success",
                "cancelled" => "crud-tag--danger",
                _ => "crud-tag--neutral"
            };
        }

        public string GetScheduleStatusLabel(string? rawStatus)
        {
            var status = (rawStatus ?? string.Empty).ToLowerInvariant();
            return status switch
            {
                "pending" => "Đang chờ duyệt",
                "confirmed" => "Đã được duyệt",
                "completed" => "Đã hoàn tất",
                "cancelled" => "Đã hủy",
                _ => "Đang cập nhật"
            };
        }

        public string GetDepositStatusBadgeClass()
        {
            return PendingDepositCount > 0 ? "warning" : "success";
        }
    }
}
