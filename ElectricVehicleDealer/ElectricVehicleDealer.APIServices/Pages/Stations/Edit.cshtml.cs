using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Stations
{
    public class EditModel : PageModel
    {
        private readonly IStationService _stationService;

        public EditModel(IStationService stationService)
        {
            _stationService = stationService;
        }

        public StationDTO? Station { get; set; }

        [BindProperty]
        public StationDTO Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Station = await _stationService.GetByIdAsync(id);
            if (Station == null)
            {
                return NotFound();
            }

            Input = new StationDTO
            {
                StationId = Station.StationId,
                StationName = Station.StationName,
                Location = Station.Location,
                ContactNumber = Station.ContactNumber,
                IsActive = Station.IsActive
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Station = await _stationService.GetByIdAsync(id);
                return Page();
            }

            var success = await _stationService.UpdateAsync(id, Input);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
