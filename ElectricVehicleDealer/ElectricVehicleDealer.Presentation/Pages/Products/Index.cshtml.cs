using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public IndexModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public List<VehicleDTO> Vehicles { get; set; } = new();

        public async Task OnGetAsync()
        {
            Vehicles = await _vehicleService.GetAllAsync();
        }

        public async Task<IActionResult> OnGetListAsync()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return new JsonResult(vehicles);
        }
    }
}
