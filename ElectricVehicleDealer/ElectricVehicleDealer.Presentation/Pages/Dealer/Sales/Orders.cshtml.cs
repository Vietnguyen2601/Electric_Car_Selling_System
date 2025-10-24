using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Dealer.Sales
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrdersModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IReadOnlyList<OrderDto> Orders { get; private set; } = Array.Empty<OrderDto>();

        public int ActiveOrders => Orders.Count(order => string.Equals(order.Status, "Đang xử lý", StringComparison.OrdinalIgnoreCase) ||
                                                         string.Equals(order.Status, "Chuẩn bị giao xe", StringComparison.OrdinalIgnoreCase) ||
                                                         string.Equals(order.Status, "Đang đặt hàng", StringComparison.OrdinalIgnoreCase));

        public int CompletedOrders => Orders.Count(order => string.Equals(order.Status, "Đã bàn giao", StringComparison.OrdinalIgnoreCase));

        public int OnHoldOrders => Orders.Count(order => string.Equals(order.Status, "Tạm dừng", StringComparison.OrdinalIgnoreCase));

        public async Task OnGetAsync()
        {
            var data = await _orderService.GetAllAsync();
            Orders = data
                .OrderByDescending(order => order.OrderDate)
                .ThenByDescending(order => order.CreatedAt)
                .ToList();
        }

        public string GetStatusClass(string? status)
        {
            var value = status?.Trim().ToLowerInvariant() ?? string.Empty;

            return value switch
            {
                "đã bàn giao" or "hoàn tất" or "completed" => "crud-tag--success",
                "tạm dừng" or "pending review" or "hold" => "crud-tag--danger",
                "đang đặt hàng" or "đang xử lý" or "chờ kiểm tra chất lượng" or "chuẩn bị giao xe" or "processing" => "crud-tag--warning",
                _ => "crud-tag--neutral"
            };
        }

        public string GetStatusLabel(string? status)
        {
            return string.IsNullOrWhiteSpace(status)
                ? "Không rõ"
                : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(status.Trim());
        }
    }
}
