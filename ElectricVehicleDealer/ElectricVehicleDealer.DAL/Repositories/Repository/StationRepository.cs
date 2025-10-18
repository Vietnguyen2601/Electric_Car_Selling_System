using ElectricVehicleDealer.DAL.DBContext;
using ElectricVehicleDealer.DAL.Models;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ElectricVehicleDealer.DAL.Repositories.Repository
{
    public class StationRepository : IStationRepository
    {
        private readonly ElectricVehicleDContext _context;

        public StationRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Station>> GetAllAsync()
        {
            return await _context.Stations
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Station?> GetByIdAsync(int id)
        {
            return await _context.Stations
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StationId == id);
        }

        public async Task<Station> CreateAsync(Station station)
        {
            station.CreatedAt = DateTime.UtcNow;
            station.IsActive = true;

            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return station;
        }

        public async Task<Station?> UpdateAsync(Station station)
        {
            var existing = await _context.Stations.FindAsync(station.StationId);
            if (existing == null) return null;

            existing.StationName = station.StationName;
            existing.Location = station.Location;
            existing.ContactNumber = station.ContactNumber;
            existing.IsActive = station.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Stations.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Stations.FindAsync(id);
            if (entity == null) return false;

            _context.Stations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
