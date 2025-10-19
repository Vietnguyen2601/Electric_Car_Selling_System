using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly IStationCarService _stationCarService;
        private readonly IPromotionService _promotionService;

        public EditModel(
            IOrderService orderService,
            IAccountService accountService,
            IStationCarService stationCarService,
            IPromotionService promotionService)
        {
            _orderService = orderService;
            _accountService = accountService;
            _stationCarService = stationCarService;
            _promotionService = promotionService;
        }

        public OrderDto? Order { get; set; }

        [BindProperty]
        public UpdateOrderDto Input { get; set; } = new();

        public IEnumerable<SelectListItem> StaffOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> StationCarOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> PromotionOptions { get; set; } = Enumerable.Empty<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _orderService.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound();
            }

            Input.StaffId = Order.StaffId;
            Input.StationCarId = Order.StationCarId;
            Input.OrderDate = Order.OrderDate;
            Input.TotalPrice = Order.TotalPrice;
            Input.Status = Order.Status;
            Input.PromotionId = Order.PromotionId;
            Input.IsActive = Order.IsActive;

            await LoadLookupsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Order = await _orderService.GetByIdAsync(id);
            await LoadLookupsAsync();

            if (Order == null)
            {
                return NotFound();
            }

            if (!Input.StaffId.HasValue || Input.StaffId <= 0)
            {
                ModelState.AddModelError(nameof(Input.StaffId), "Staff is required.");
            }

            if (!Input.StationCarId.HasValue || Input.StationCarId <= 0)
            {
                ModelState.AddModelError(nameof(Input.StationCarId), "Station car is required.");
            }

            if (!Input.OrderDate.HasValue)
            {
                ModelState.AddModelError(nameof(Input.OrderDate), "Order date is required.");
            }

            if (!Input.TotalPrice.HasValue)
            {
                ModelState.AddModelError(nameof(Input.TotalPrice), "Total price is required.");
            }

            if (string.IsNullOrWhiteSpace(Input.Status))
            {
                ModelState.AddModelError(nameof(Input.Status), "Status is required.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.PromotionId == 0)
            {
                Input.PromotionId = null;
            }

            var success = await _orderService.UpdateAsync(id, Input);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }

        private async Task LoadLookupsAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountItems = accounts
                .Select(a => new SelectListItem($"{a.Username} (#{a.AccountId})", a.AccountId.ToString()))
                .ToList();
            StaffOptions = accountItems;

            var stationCars = await _stationCarService.GetAllAsync();
            StationCarOptions = stationCars
                .Select(sc => new SelectListItem(
                    $"{sc.StationName} - {sc.VehicleModel} (#{sc.StationCarId})",
                    sc.StationCarId.ToString()))
                .ToList();

            var promotions = await _promotionService.GetAllAsync();
            PromotionOptions = promotions
                .Select(p => new SelectListItem(
                    $"{p.PromoCode} ({p.DiscountPercentage}%)",
                    p.PromotionId.ToString()))
                .ToList();
        }
    }
}
