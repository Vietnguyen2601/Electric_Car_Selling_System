using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Products.Catalog
{
    public class EditModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public EditModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
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

            return RedirectToPage("/Evm/Products/Catalog");
        }
    }
}
