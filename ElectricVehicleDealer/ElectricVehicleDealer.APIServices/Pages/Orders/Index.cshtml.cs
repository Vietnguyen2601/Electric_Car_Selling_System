using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<OrderDto> Orders { get; set; } = new List<OrderDto>();

        public async Task OnGetAsync()
        {
            var data = await _orderService.GetAllAsync();
            Orders = new List<OrderDto>(data);
        }
    }
}
