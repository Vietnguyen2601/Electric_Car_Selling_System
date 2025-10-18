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
    }
}
