using ElectricVehicleDealer.Common.DTOs.PromotionDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionDto>> GetAllAsync();
        Task<PromotionDto?> GetByIdAsync(int id);
        Task<PromotionDto> CreateAsync(CreatePromotionDto dto);
        Task<bool> UpdateAsync(int id, UpdatePromotionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
