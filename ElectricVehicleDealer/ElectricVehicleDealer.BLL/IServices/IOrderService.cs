using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(CreateOrderDto dto);
        Task<bool> UpdateAsync(int id, UpdateOrderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
