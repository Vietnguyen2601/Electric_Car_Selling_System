using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Stations
{
    public class CreateModel : PageModel
    {
        private readonly IStationService _stationService;

        public CreateModel(IStationService stationService)
        {
            _stationService = stationService;
        }

        [BindProperty]
        public StationDTO Input { get; set; } = new() { IsActive = true };

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _stationService.CreateAsync(Input);
            return RedirectToPage("Index");
        }
    }
}
