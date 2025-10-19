using ElectricVehicleDealer.Common.DTOs.ScheduleDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetAllAsync();
        Task<ScheduleDto?> GetByIdAsync(int id);
        Task<ScheduleDto> CreateAsync(CreateScheduleDto dto);
        Task<bool> UpdateAsync(int id, UpdateScheduleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
