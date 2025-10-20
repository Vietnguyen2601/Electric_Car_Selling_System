using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Evm.Products
{
    public class CatalogModel : PageModel
    {
        private readonly IVehicleService _vehicleService;
        private readonly IStationService _stationService;
        private readonly IStationCarService _stationCarService;

        public CatalogModel(
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
                ModelState.AddModelError(nameof(SelectedVehicleId), "Vui lòng chọn xe.");
            }

            if (SelectedStationId <= 0)
            {
                ModelState.AddModelError(nameof(SelectedStationId), "Vui lòng chọn trạm.");
            }

            if (Quantity <= 0)
            {
                ModelState.AddModelError(nameof(Quantity), "Số lượng phải lớn hơn 0.");
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
                    SuccessMessage = "Cập nhật phân bổ xe thành công.";
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
                    SuccessMessage = "Thêm phân bổ xe mới thành công.";
                }
            }
            catch
            {
                ErrorMessage = "Không thể lưu phân bổ xe. Vui lòng thử lại.";
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
