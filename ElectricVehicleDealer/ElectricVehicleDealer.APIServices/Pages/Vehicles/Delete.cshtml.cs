using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Vehicles
{
    public class DeleteModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public DeleteModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
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

            return RedirectToPage("Index");
        }
    }
}
