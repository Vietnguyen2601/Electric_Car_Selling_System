using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Stations
{
    public class DeleteModel : PageModel
    {
        private readonly IStationService _stationService;

        public DeleteModel(IStationService stationService)
        {
            _stationService = stationService;
        }

        public StationDTO? Station { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Station = await _stationService.GetByIdAsync(id);
            if (Station == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _stationService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
