using ElectricVehicleDealer.Common.DTOs.StationDtos;


namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IStationService
    {
        Task<IEnumerable<StationDTO>> GetAllAsync();
        Task<StationDTO?> GetByIdAsync(int id);
        Task<StationDTO> CreateAsync(StationDTO dto);
        Task<bool> UpdateAsync(int id, StationDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
