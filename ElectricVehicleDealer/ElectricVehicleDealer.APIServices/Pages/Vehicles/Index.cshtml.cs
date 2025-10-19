using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStationService _stationService;
        private readonly IStationCarService _stationCarService;

        public IndexModel(
            IVehicleService vehicleService,
            IStationService stationService,
            IStationCarService stationCarService)
        {
            _vehicleService = vehicleService;
            _stationService = stationService;
            _stationCarService = stationCarService;
        }

        public IList<VehicleDTO> Vehicles { get; private set; } = new List<VehicleDTO>();
        public List<StationDTO> Stations { get; private set; } = new();
        public List<StationCarDTO> StationAssignments { get; private set; } = new();

        [BindProperty]
        public int SelectedVehicleId { get; set; }

        [BindProperty]
        public int SelectedStationId { get; set; }

        [BindProperty]
        public int Quantity { get; set; } = 1;

        [TempData]
        public string? SuccessMessage { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        public async Task<IActionResult> OnPostAssignAsync()
        {
            await LoadDataAsync();

            if (SelectedVehicleId <= 0)
            {
                ModelState.AddModelError(nameof(SelectedVehicleId), "Vui l�ng ch?n xe.");
            }

            if (SelectedStationId <= 0)
            {
                ModelState.AddModelError(nameof(SelectedStationId), "Vui l�ng ch?n tr?m.");
            }

            if (Quantity <= 0)
            {
                ModelState.AddModelError(nameof(Quantity), "S? lu?ng ph?i l?n h?n 0.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existing = StationAssignments.FirstOrDefault(a =>
                a.VehicleId == SelectedVehicleId &&
                a.StationId == SelectedStationId);

            try
            {
                if (existing != null)
                {
                    existing.Quantity = Quantity;
                    existing.IsActive = true;
                    await _stationCarService.UpdateAsync(existing.StationCarId, existing);
                    SuccessMessage = "C?p nh?t ph�n b? xe th�nh c�ng.";
                }
                else
                {
                    var createDto = new StationCarDTO
                    {
                        VehicleId = SelectedVehicleId,
                        StationId = SelectedStationId,
                        Quantity = Quantity,
                        IsActive = true
                    };

                    await _stationCarService.CreateAsync(createDto);
                    SuccessMessage = "Th�m ph�n b? xe m?i th�nh c�ng.";
                }
            }
            catch
            {
                ErrorMessage = "Kh�ng th? l?u ph�n b? xe. Vui l�ng th? l?i.";
                return Page();
            }

            return RedirectToPage();
        }

        private async Task LoadDataAsync()
        {
            Vehicles = await _vehicleService.GetAllAsync();

            Stations = (await _stationService.GetAllAsync()).ToList();

            StationAssignments = (await _stationCarService.GetAllAsync())
                .OrderBy(sc => sc.VehicleModel)
                .ThenBy(sc => sc.StationName)
                .ToList();

            if (SelectedVehicleId <= 0 && Vehicles.Any())
            {
                SelectedVehicleId = Vehicles.First().VehicleId;
            }

            if (SelectedStationId <= 0 && Stations.Any())
            {
                SelectedStationId = Stations.First().StationId;
            }

            if (Quantity <= 0)
            {
                Quantity = 1;
            }
        }
    }
}
