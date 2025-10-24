using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectricVehicleDealer.Presentation.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly IStationCarService _stationCarService;
        private readonly IPromotionService _promotionService;

        public CreateModel(
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

        [BindProperty]
        public CreateOrderDto Input { get; set; } = new()
        {
            OrderDate = DateTime.UtcNow,
            IsActive = true,
            Status = "Pending"
        };

        public IEnumerable<SelectListItem> CustomerOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> StaffOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> StationCarOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> PromotionOptions { get; set; } = Enumerable.Empty<SelectListItem>();

        public async Task OnGetAsync()
        {
            await LoadLookupsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadLookupsAsync();

            if (Input.CustomerId <= 0)
            {
                ModelState.AddModelError(nameof(Input.CustomerId), "Customer is required.");
            }

            if (!Input.StaffId.HasValue || Input.StaffId.Value <= 0)
            {
                ModelState.AddModelError(nameof(Input.StaffId), "Staff is required.");
            }

            if (Input.StationCarId <= 0)
            {
                ModelState.AddModelError(nameof(Input.StationCarId), "Station car is required.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.PromotionId == 0)
            {
                Input.PromotionId = null;
            }

            await _orderService.CreateAsync(Input);
            return RedirectToPage("Index");
        }

        private async Task LoadLookupsAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            CustomerOptions = accounts
                .Select(a => new SelectListItem($"{a.Username} (#{a.AccountId})", a.AccountId.ToString()))
                .ToList();
            StaffOptions = CustomerOptions;

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
