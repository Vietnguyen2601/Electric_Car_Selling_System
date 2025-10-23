using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Stations
{
    public class IndexModel : PageModel
    {
        private readonly IStationService _stationService;

        public IndexModel(IStationService stationService)
        {
            _stationService = stationService;
        }

        public IList<StationDTO> Stations { get; set; } = new List<StationDTO>();

        public async Task OnGetAsync()
        {
            var data = await _stationService.GetAllAsync();
            Stations = new List<StationDTO>(data);
        }
    }
}
