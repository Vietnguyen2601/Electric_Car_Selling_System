using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IScheduleRepository
    {
        Task<List<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(int id);
        Task<Schedule> CreateAsync(Schedule entity);
        Task<Schedule?> UpdateAsync(Schedule entity);
        Task<bool> DeleteAsync(int id);
    }
}
