using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStationCarService _stationCarService;

        public DetailModel(IVehicleService vehicleService, IStationCarService stationCarService)
        {
            _vehicleService = vehicleService;
            _stationCarService = stationCarService;
        }

        [BindProperty]
        public VehicleDTO? Vehicle { get; set; }
        public List<StationCarDTO> StationCars { get; set; } = new();

        public async Task OnGetAsync(int id)
        {
            Vehicle = await _vehicleService.GetByIdAsync(id);

            if (Vehicle != null)
            {
                StationCars = (await _stationCarService.GetByVehicleIdAsync(Vehicle.VehicleId)).ToList();
            }
        }
    }
}
