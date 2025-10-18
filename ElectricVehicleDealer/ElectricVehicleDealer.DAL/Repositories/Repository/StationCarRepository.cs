using ElectricVehicleDealer.DAL.DBContext;
using ElectricVehicleDealer.DAL.Models;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;


namespace ElectricVehicleDealer.DAL.Repositories.Repository
{
    public class StationCarRepository : IStationCarRepository
    {
        private readonly ElectricVehicleDContext _context;

        public StationCarRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StationCar>> GetAllAsync()
        {
            return await _context.StationCars
                .Include(sc => sc.Station)
                .Include(sc => sc.Vehicle)
                .Where(sc => sc.IsActive)
                .ToListAsync();
        }

        public async Task<StationCar?> GetByIdAsync(int id)
        {
            return await _context.StationCars
                .Include(sc => sc.Station)
                .Include(sc => sc.Vehicle)
                .FirstOrDefaultAsync(sc => sc.StationCarId == id && sc.IsActive);
        }

        public async Task<IEnumerable<StationCar>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _context.StationCars
                .Include(sc => sc.Station)
                .Include(sc => sc.Vehicle)
                .Where(sc => sc.VehicleId == vehicleId && sc.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(StationCar entity)
        {
            _context.StationCars.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StationCar entity)
        {
            _context.StationCars.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(StationCar entity)
        {
            _context.StationCars.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
