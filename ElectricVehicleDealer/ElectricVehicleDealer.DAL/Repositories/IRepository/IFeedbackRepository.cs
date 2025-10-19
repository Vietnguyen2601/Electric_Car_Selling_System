using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(int id);
        Task<Feedback> CreateAsync(Feedback entity);
        Task<Feedback?> UpdateAsync(Feedback entity);
        Task<bool> DeleteAsync(int id);
    }
}
