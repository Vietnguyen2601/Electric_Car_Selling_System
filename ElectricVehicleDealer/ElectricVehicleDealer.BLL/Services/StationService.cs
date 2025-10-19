using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;

namespace ElectricVehicleDealer.BLL.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;

        public StationService(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<IEnumerable<StationDTO>> GetAllAsync()
        {
            var stations = await _stationRepository.GetAllAsync();
            return stations.ToDTOList();
        }

        public async Task<StationDTO?> GetByIdAsync(int id)
        {
            var station = await _stationRepository.GetByIdAsync(id);
            return station?.ToDTO();
        }

        public async Task<StationDTO> CreateAsync(StationDTO dto)
        {
            var entity = dto.ToEntity();
            await _stationRepository.CreateAsync(entity);
            return entity.ToDTO();
        }

        public async Task<bool> UpdateAsync(int id, StationDTO dto)
        {
            var existing = await _stationRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.StationName = dto.StationName;
            existing.Location = dto.Location;
            existing.ContactNumber = dto.ContactNumber;
            existing.IsActive = dto.IsActive;
            existing.UpdatedAt = System.DateTime.UtcNow;

            await _stationRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _stationRepository.DeleteAsync(id);
        }
    }
}
