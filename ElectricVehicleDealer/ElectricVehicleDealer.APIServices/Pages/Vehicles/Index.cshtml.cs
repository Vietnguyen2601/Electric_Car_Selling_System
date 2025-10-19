using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly IVehicleService _vehicleService;

        public IndexModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public IList<VehicleDTO> Vehicles { get; set; } = new List<VehicleDTO>();

        public async Task OnGetAsync()
        {
            Vehicles = await _vehicleService.GetAllAsync();
        }
    }
}
