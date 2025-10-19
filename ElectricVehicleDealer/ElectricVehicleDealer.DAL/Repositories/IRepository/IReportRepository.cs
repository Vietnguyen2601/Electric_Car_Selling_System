using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IReportRepository
    {
        Task<List<Report>> GetAllAsync();
        Task<Report?> GetByIdAsync(int id);
        Task<Report> CreateAsync(Report entity);
        Task<Report?> UpdateAsync(Report entity);
        Task<bool> DeleteAsync(int id);
    }
}
