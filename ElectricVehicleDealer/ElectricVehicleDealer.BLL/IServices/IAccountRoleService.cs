using ElectricVehicleDealer.Common.DTOs.AccountRoleDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IAccountRoleService
    {
        Task<IEnumerable<AccountRoleDto>> GetAllAsync();
        Task<AccountRoleDto?> GetByIdAsync(int id);
        Task<AccountRoleDto> CreateAsync(CreateAccountRoleDto dto);
        Task<bool> UpdateAsync(int id, UpdateAccountRoleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
