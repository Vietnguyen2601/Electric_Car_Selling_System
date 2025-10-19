using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using ElectricVehicleDealer.DAL.Repositories.IRepository;


namespace ElectricVehicleDealer.BLL.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VehicleDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllVehiclesAsync();
            return entities.ToDTOList();
        }

        public async Task<VehicleDTO?> GetByIdAsync(int id)
        {
            var v = await _repository.GetVehicleByIdAsync(id);
            return v?.ToDTO();
        }

        public async Task<VehicleDTO> CreateAsync(CreateVehicleDto dto)
        {
            var entity = dto.ToEntity();
            await _repository.AddVehicleAsync(entity);
            return entity.ToDTO();
        }

        public async Task<bool> UpdateAsync(int id, UpdateVehicleDto dto)
        {
            var existing = await _repository.GetVehicleByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            dto.MapToEntity(existing);
            await _repository.UpdateVehicleAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetVehicleByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            await _repository.DeleteVehicleAsync(id);
            return true;
        }
    }
}
