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
    public class ReportRepository : IReportRepository
    {
        private readonly ElectricVehicleDContext _context;

        public ReportRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.Account)
                .Where(r => r.IsActive)
                .ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(int id)
        {
            return await _context.Reports
                .Include(r => r.Account)
                .FirstOrDefaultAsync(r => r.ReportId == id && r.IsActive);
        }

        public async Task<Report> CreateAsync(Report entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Reports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Report?> UpdateAsync(Report entity)
        {
            var existing = await _context.Reports.FindAsync(entity.ReportId);
            if (existing == null) return null;

            existing.ReportType = entity.ReportType;
            existing.GeneratedDate = entity.GeneratedDate;
            existing.Data = entity.Data;
            existing.AccountId = entity.AccountId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Reports.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Reports.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.Reports.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
