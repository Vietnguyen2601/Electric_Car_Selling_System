using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> CreateAsync(Payment entity);
        Task<Payment?> UpdateAsync(Payment entity);
        Task<bool> DeleteAsync(int id);
    }
}
