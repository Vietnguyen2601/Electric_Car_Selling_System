using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using ElectricVehicleDealer.Presentation.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Products.Catalog
{
    public class EditModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IHubContext<VehicleHub> _vehicleHubContext;

        public EditModel(IVehicleService vehicleService, IHubContext<VehicleHub> vehicleHubContext)
        {
            _vehicleService = vehicleService;
            _vehicleHubContext = vehicleHubContext;
        }

        public VehicleDTO? Vehicle { get; set; }

        [BindProperty]
        public UpdateVehicleDto Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vehicle = await _vehicleService.GetByIdAsync(id);
            if (Vehicle == null)
            {
                return NotFound();
            }

            Input.Model = Vehicle.Model;
            Input.Type = Vehicle.Type;
            Input.Color = Vehicle.Color;
            Input.Price = Vehicle.Price;
            Input.IsActive = Vehicle.IsActive;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Vehicle = await _vehicleService.GetByIdAsync(id);
                return Page();
            }

            var success = await _vehicleService.UpdateAsync(id, Input);
            if (!success)
            {
                return NotFound();
            }

            await _vehicleHubContext.Clients.All.SendAsync(VehicleHub.VehicleCatalogChangedEvent);
            return RedirectToPage("/Evm/Products/Catalog");
        }
    }
}
