using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }





        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.ToDtoList();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.ToDto();
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var entity = dto.ToEntity();
            var created = await _repository.CreateAsync(entity);
            return created.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            dto.MapToEntity(existing);
            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
