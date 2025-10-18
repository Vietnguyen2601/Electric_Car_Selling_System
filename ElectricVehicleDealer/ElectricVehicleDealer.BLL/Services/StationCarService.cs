using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;


namespace ElectricVehicleDealer.BLL.Services
{
    public class StationCarService : IStationCarService
    {
        private readonly IStationCarRepository _stationCarRepository;

        public StationCarService(IStationCarRepository stationCarRepository)
        {
            _stationCarRepository = stationCarRepository;
        }

        public async Task<IEnumerable<StationCarDTO>> GetAllAsync()
        {
            var list = await _stationCarRepository.GetAllAsync();
            return list.ToDTOList();
        }

        public async Task<StationCarDTO?> GetByIdAsync(int id)
        {
            var entity = await _stationCarRepository.GetByIdAsync(id);
            return entity?.ToDTO();
        }

        public async Task<IEnumerable<StationCarDTO>> GetByVehicleIdAsync(int vehicleId)
        {
            var entities = await _stationCarRepository.GetByVehicleIdAsync(vehicleId);
            return entities.ToDTOList();
        }


        public async Task<StationCarDTO> CreateAsync(StationCarDTO dto)
        {
            var entity = dto.ToEntity();
            entity.CreatedAt = System.DateTime.UtcNow;

            await _stationCarRepository.AddAsync(entity);
            return entity.ToDTO();
        }

        public async Task<bool> UpdateAsync(int id, StationCarDTO dto)
        {
            var existing = await _stationCarRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.VehicleId = dto.VehicleId;
            existing.StationId = dto.StationId;
            existing.Quantity = dto.Quantity;
            existing.IsActive = dto.IsActive;
            existing.UpdatedAt = System.DateTime.UtcNow;

            await _stationCarRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _stationCarRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _stationCarRepository.DeleteAsync(existing);
            return true;
        }
    }
}
