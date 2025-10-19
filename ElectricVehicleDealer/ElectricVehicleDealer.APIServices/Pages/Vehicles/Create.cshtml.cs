using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Vehicles
{
    public class CreateModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public CreateModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
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
            return RedirectToPage("Index");
        }
    }
}
