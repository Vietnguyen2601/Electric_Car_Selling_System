using ElectricVehicleDealer.Common.DTOs.StaffRevenueDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IStaffRevenueService
    {
        Task<IEnumerable<StaffRevenueDto>> GetAllAsync();
        Task<StaffRevenueDto?> GetByIdAsync(int id);
        Task<StaffRevenueDto> CreateAsync(CreateStaffRevenueDto dto);
        Task<bool> UpdateAsync(int id, UpdateStaffRevenueDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
