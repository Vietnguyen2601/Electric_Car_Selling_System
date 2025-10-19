using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.ScheduleDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repository;

        public ScheduleService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.ToDtoList();
        }

        public async Task<ScheduleDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.ToDto();
        }

        public async Task<ScheduleDto> CreateAsync(CreateScheduleDto dto)
        {
            var entity = dto.ToEntity();
            var created = await _repository.CreateAsync(entity);
            return created.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, UpdateScheduleDto dto)
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
