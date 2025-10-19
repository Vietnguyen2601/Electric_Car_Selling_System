using ElectricVehicleDealer.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ElectricVehicleDealer.Presentation.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStationCarService _stationCarService;
        private readonly IOrderService _orderService;

        private const decimal DepositPercentage = 0.1m;

        public DetailModel(
            IVehicleService vehicleService,
            IStationCarService stationCarService,
            IOrderService orderService)
        {
            _vehicleService = vehicleService;
            _stationCarService = stationCarService;
            _orderService = orderService;
        }

        [BindProperty]
        public VehicleDTO? Vehicle { get; set; }

        public List<StationCarDTO> StationCars { get; set; } = new();

        [BindProperty]
        public int SelectedStationCarId { get; set; }

        [TempData]
        public string? SuccessMessage { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public decimal? CalculatedDeposit => Vehicle != null
            ? Math.Round(Vehicle.Price * DepositPercentage, 0)
            : null;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var loaded = await LoadPageDataAsync(id);
            if (!loaded)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostReserveAsync(int id)
        {
            var loaded = await LoadPageDataAsync(id);
            if (!loaded)
            {
                return NotFound();
            }

            if (SelectedStationCarId <= 0)
            {
                ModelState.AddModelError(nameof(SelectedStationCarId), "Vui lòng chọn trạm có xe.");
                return Page();
            }

            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (!accountId.HasValue)
            {
                var returnUrl = Url.Page("/Products/Detail", new { id });
                return RedirectToPage("/Store/Login", new { returnUrl });
            }

            var stationCar = await _stationCarService.GetByIdAsync(SelectedStationCarId);
            if (stationCar == null || stationCar.VehicleId != Vehicle!.VehicleId)
            {
                ErrorMessage = "Trạm đã chọn không khả dụng cho mẫu xe này.";
                return RedirectToPage(new { id });
            }

            if (stationCar.Quantity <= 0)
            {
                ErrorMessage = "Trạm đã hết xe khả dụng. Vui lòng chọn trạm khác.";
                return RedirectToPage(new { id });
            }

            var createDto = new CreateOrderDto
            {
                CustomerId = accountId.Value,
                StationCarId = stationCar.StationCarId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = Math.Round(Vehicle!.Price * DepositPercentage, 0),
                Status = "Pending",
                PromotionId = null,
                StaffId = null,
                IsActive = true
            };

            try
            {
                var created = await _orderService.CreateAsync(createDto);

                stationCar.Quantity -= 1;
                var updated = await _stationCarService.UpdateAsync(stationCar.StationCarId, stationCar);
                if (!updated)
                {
                    await _orderService.DeleteAsync(created.OrderId);
                    ErrorMessage = "Không thể cập nhật số lượng xe. Vui lòng thử lại.";
                    return RedirectToPage(new { id });
                }

                SuccessMessage = "Đặt cọc thành công! Đơn hàng đang chờ nhân viên tiếp nhận.";
            }
            catch
            {
                ErrorMessage = "Không thể đặt cọc hiện tại. Vui lòng thử lại sau.";
            }

            return RedirectToPage(new { id });
        }

        private async Task<bool> LoadPageDataAsync(int id)
        {
            Vehicle = await _vehicleService.GetByIdAsync(id);

            if (Vehicle != null)
            {
                StationCars = (await _stationCarService.GetByVehicleIdAsync(Vehicle.VehicleId))
                    .OrderByDescending(sc => sc.Quantity > 0)
                    .ThenBy(sc => sc.StationName)
                    .ToList();

                if (StationCars.Any(sc => sc.Quantity > 0) && SelectedStationCarId <= 0)
                {
                    SelectedStationCarId = StationCars.First(sc => sc.Quantity > 0).StationCarId;
                }
            }

            return Vehicle != null;
        }
    }
}
