using ElectricVehicleDealer.Common.DTOs.FeedbackDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackDto>> GetAllAsync();
        Task<FeedbackDto?> GetByIdAsync(int id);
        Task<FeedbackDto> CreateAsync(CreateFeedbackDto dto);
        Task<bool> UpdateAsync(int id, UpdateFeedbackDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
