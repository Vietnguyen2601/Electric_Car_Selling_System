using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.AccountRoleDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        private readonly IAccountRoleRepository _repository;

        public AccountRoleService(IAccountRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountRoleDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.ToDtoList();
        }

        public async Task<AccountRoleDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.ToDto();
        }

        public async Task<AccountRoleDto> CreateAsync(CreateAccountRoleDto dto)
        {
            var entity = dto.ToEntity();
            var created = await _repository.CreateAsync(entity);
            return created.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccountRoleDto dto)
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
