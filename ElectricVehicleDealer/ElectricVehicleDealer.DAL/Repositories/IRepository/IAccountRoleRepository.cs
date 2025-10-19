using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IAccountRoleRepository
    {
        Task<List<AccountRole>> GetAllAsync();
        Task<AccountRole?> GetByIdAsync(int id);
        Task<AccountRole> CreateAsync(AccountRole entity);
        Task<AccountRole?> UpdateAsync(AccountRole entity);
        Task<bool> DeleteAsync(int id);
    }
}
