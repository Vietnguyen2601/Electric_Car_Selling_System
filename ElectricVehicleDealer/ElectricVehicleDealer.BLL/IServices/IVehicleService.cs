using ElectricVehicleDealer.Common.DTOs.VehicleDtos;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAllAsync();
        Task<VehicleDTO?> GetByIdAsync(int id);
        Task<VehicleDTO> CreateAsync(CreateVehicleDto dto);
        Task<bool> UpdateAsync(int id, UpdateVehicleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
