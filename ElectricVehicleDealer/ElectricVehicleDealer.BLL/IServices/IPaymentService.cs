using ElectricVehicleDealer.Common.DTOs.PaymentDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto?> GetByIdAsync(int id);
        Task<PaymentDto> CreateAsync(CreatePaymentDto dto);
        Task<bool> UpdateAsync(int id, UpdatePaymentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
