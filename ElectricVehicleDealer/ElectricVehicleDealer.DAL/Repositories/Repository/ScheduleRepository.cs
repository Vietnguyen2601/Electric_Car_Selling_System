using ElectricVehicleDealer.DAL.DBContext;
using ElectricVehicleDealer.DAL.Models;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ElectricVehicleDContext _context;

        public ScheduleRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(s => s.Customer)
                .Include(s => s.StationCar)
                    .ThenInclude(sc => sc.Station)
                .Include(s => s.StationCar)
                    .ThenInclude(sc => sc.Vehicle)
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            return await _context.Schedules
                .Include(s => s.Customer)
                .Include(s => s.StationCar)
                    .ThenInclude(sc => sc.Station)
                .Include(s => s.StationCar)
                    .ThenInclude(sc => sc.Vehicle)
                .FirstOrDefaultAsync(s => s.ScheduleId == id && s.IsActive);
        }

        public async Task<Schedule> CreateAsync(Schedule entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Schedules.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Schedule?> UpdateAsync(Schedule entity)
        {
            var existing = await _context.Schedules.FindAsync(entity.ScheduleId);
            if (existing == null) return null;

            existing.CustomerId = entity.CustomerId;
            existing.StationCarId = entity.StationCarId;
            existing.Status = entity.Status;
            existing.ScheduleTime = entity.ScheduleTime;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Schedules.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Schedules.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.Schedules.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
