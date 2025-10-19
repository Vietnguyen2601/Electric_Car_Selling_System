using ElectricVehicleDealer.BLL.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IVehicleService _vehicleService;
        private readonly IOrderService _orderService;
        private readonly IStationService _stationService;

        public IndexModel(
            IAccountService accountService,
            IVehicleService vehicleService,
            IOrderService orderService,
            IStationService stationService)
        {
            _accountService = accountService;
            _vehicleService = vehicleService;
            _orderService = orderService;
            _stationService = stationService;
        }

        public int AccountCount { get; private set; }
        public int VehicleCount { get; private set; }
        public int OrderCount { get; private set; }
        public int StationCount { get; private set; }

        public async Task OnGetAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            AccountCount = accounts.Count();

            var vehicles = await _vehicleService.GetAllAsync();
            VehicleCount = vehicles.Count;

            var orders = await _orderService.GetAllAsync();
            OrderCount = orders.Count();

            var stations = await _stationService.GetAllAsync();
            StationCount = stations.Count();
        }
    }
}
