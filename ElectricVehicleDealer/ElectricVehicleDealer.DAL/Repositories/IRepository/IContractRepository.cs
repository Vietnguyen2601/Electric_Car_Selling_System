using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAllAsync();
        Task<Contract?> GetByIdAsync(int id);
        Task<Contract> CreateAsync(Contract entity);
        Task<Contract?> UpdateAsync(Contract entity);
        Task<bool> DeleteAsync(int id);
    }
}
