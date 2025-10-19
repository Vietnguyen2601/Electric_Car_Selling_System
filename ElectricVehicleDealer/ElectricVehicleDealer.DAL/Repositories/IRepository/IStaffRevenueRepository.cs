using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IStaffRevenueRepository
    {
        Task<List<StaffRevenue>> GetAllAsync();
        Task<StaffRevenue?> GetByIdAsync(int id);
        Task<StaffRevenue> CreateAsync(StaffRevenue entity);
        Task<StaffRevenue?> UpdateAsync(StaffRevenue entity);
        Task<bool> DeleteAsync(int id);
    }
}
