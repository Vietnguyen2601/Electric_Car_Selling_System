using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.RoleDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.ToDtoList();
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.ToDto();
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var entity = dto.ToEntity();
            var created = await _repository.CreateAsync(entity);
            return created.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, UpdateRoleDto dto)
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
