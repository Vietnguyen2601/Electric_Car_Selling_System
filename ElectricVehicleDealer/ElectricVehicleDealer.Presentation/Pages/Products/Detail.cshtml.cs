using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using ElectricVehicleDealer.Common.DTOs.ScheduleDtos;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStationCarService _stationCarService;
        private readonly IOrderService _orderService;
        private readonly IScheduleService _scheduleService;

        private const decimal DepositPercentage = 0.1m;

        public DetailModel(
            IVehicleService vehicleService,
            IStationCarService stationCarService,
            IOrderService orderService,
            IScheduleService scheduleService)
        {
            _vehicleService = vehicleService;
            _stationCarService = stationCarService;
            _orderService = orderService;
            _scheduleService = scheduleService;
        }

        [BindProperty]
        public VehicleDTO? Vehicle { get; set; }

        public List<StationCarDTO> StationCars { get; set; } = new();

        [BindProperty]
        public int SelectedStationCarId { get; set; }

        [BindProperty]
        public int ScheduleStationCarId { get; set; }

        [BindProperty]
        public DateTime? ScheduleTime { get; set; }

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

        public async Task<IActionResult> OnPostScheduleAsync(int id)
        {
            var loaded = await LoadPageDataAsync(id);
            if (!loaded)
            {
                return NotFound();
            }

            if (ScheduleStationCarId <= 0)
            {
                ModelState.AddModelError(nameof(ScheduleStationCarId), "Vui lòng chọn trạm lái thử.");
            }

            if (!ScheduleTime.HasValue)
            {
                ModelState.AddModelError(nameof(ScheduleTime), "Vui lòng chọn thời gian lái thử.");
            }
            else if (ScheduleTime.Value <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(ScheduleTime), "Thời gian lái thử phải nằm trong tương lai.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ScheduleModalOpen"] = true;
                return Page();
            }

            var accountId = HttpContext.Session.GetInt32("AccountId");
            if (!accountId.HasValue)
            {
                var returnUrl = Url.Page("/Products/Detail", new { id });
                return RedirectToPage("/Store/Login", new { returnUrl });
            }

            var stationCar = await _stationCarService.GetByIdAsync(ScheduleStationCarId);
            if (stationCar == null || stationCar.VehicleId != Vehicle!.VehicleId)
            {
                ErrorMessage = "Trạm đã chọn không khả dụng cho mẫu xe này.";
                return RedirectToPage(new { id });
            }

            if (stationCar.Quantity <= 0)
            {
                ErrorMessage = "Trạm đã hết xe khả dụng để lái thử. Vui lòng chọn trạm khác.";
                return RedirectToPage(new { id });
            }

            var localSchedule = DateTime.SpecifyKind(ScheduleTime!.Value, DateTimeKind.Local);
            var createDto = new CreateScheduleDto
            {
                CustomerId = accountId.Value,
                StationCarId = stationCar.StationCarId,
                ScheduleTime = localSchedule.ToUniversalTime(),
                Status = "Pending",
                IsActive = true
            };

            try
            {
                await _scheduleService.CreateAsync(createDto);
                SuccessMessage = "Đặt lịch lái thử thành công! Nhân viên sẽ liên hệ để xác nhận.";
            }
            catch
            {
                ErrorMessage = "Không thể đặt lịch lái thử lúc này. Vui lòng thử lại sau.";
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

                if (StationCars.Any(sc => sc.Quantity > 0) && ScheduleStationCarId <= 0)
                {
                    ScheduleStationCarId = StationCars.First(sc => sc.Quantity > 0).StationCarId;
                }
            }

            return Vehicle != null;
        }
    }
}
