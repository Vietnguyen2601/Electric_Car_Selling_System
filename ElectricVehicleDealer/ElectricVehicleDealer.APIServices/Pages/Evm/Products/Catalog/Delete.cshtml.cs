using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using ElectricVehicleDealer.Presentation.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Products.Catalog
{
    public class DeleteModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IHubContext<VehicleHub> _vehicleHubContext;

        public DeleteModel(IVehicleService vehicleService, IHubContext<VehicleHub> vehicleHubContext)
        {
            _vehicleService = vehicleService;
            _vehicleHubContext = vehicleHubContext;
        }

        public VehicleDTO? Vehicle { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vehicle = await _vehicleService.GetByIdAsync(id);
            if (Vehicle == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _vehicleService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            await _vehicleHubContext.Clients.All.SendAsync(VehicleHub.VehicleCatalogChangedEvent);
            return RedirectToPage("/Evm/Products/Catalog");
        }
    }
}
