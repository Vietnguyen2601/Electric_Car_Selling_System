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
    public class StaffRevenueRepository : IStaffRevenueRepository
    {
        private readonly ElectricVehicleDContext _context;

        public StaffRevenueRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<StaffRevenue>> GetAllAsync()
        {
            return await _context.StaffRevenues
                .Include(sr => sr.Staff)
                .Where(sr => sr.IsActive)
                .ToListAsync();
        }

        public async Task<StaffRevenue?> GetByIdAsync(int id)
        {
            return await _context.StaffRevenues
                .Include(sr => sr.Staff)
                .FirstOrDefaultAsync(sr => sr.StaffRevenueId == id && sr.IsActive);
        }

        public async Task<StaffRevenue> CreateAsync(StaffRevenue entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.StaffRevenues.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StaffRevenue?> UpdateAsync(StaffRevenue entity)
        {
            var existing = await _context.StaffRevenues.FindAsync(entity.StaffRevenueId);
            if (existing == null) return null;

            existing.StaffId = entity.StaffId;
            existing.RevenueDate = entity.RevenueDate;
            existing.TotalRevenue = entity.TotalRevenue;
            existing.Commission = entity.Commission;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.StaffRevenues.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
