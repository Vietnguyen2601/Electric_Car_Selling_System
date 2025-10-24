using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using ElectricVehicleDealer.Presentation.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Products.Catalog
{
    public class CreateModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IHubContext<VehicleHub> _vehicleHubContext;

        public CreateModel(IVehicleService vehicleService, IHubContext<VehicleHub> vehicleHubContext)
        {
            _vehicleService = vehicleService;
            _vehicleHubContext = vehicleHubContext;
        }

        [BindProperty]
        public CreateVehicleDto Input { get; set; } = new() { IsActive = true };

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _vehicleService.CreateAsync(Input);
            await _vehicleHubContext.Clients.All.SendAsync(VehicleHub.VehicleCatalogChangedEvent);
            return RedirectToPage("/Evm/Products/Catalog");
        }
    }
}
