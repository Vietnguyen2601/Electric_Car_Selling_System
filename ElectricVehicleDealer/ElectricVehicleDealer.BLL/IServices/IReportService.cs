using ElectricVehicleDealer.Common.DTOs.ReportDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllAsync();
        Task<ReportDto?> GetByIdAsync(int id);
        Task<ReportDto> CreateAsync(CreateReportDto dto);
        Task<bool> UpdateAsync(int id, UpdateReportDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
