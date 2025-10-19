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
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ElectricVehicleDContext _context;

        public PromotionRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetAllAsync()
        {
            return await _context.Promotions
                .Include(p => p.Station)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(int id)
        {
            return await _context.Promotions
                .Include(p => p.Station)
                .FirstOrDefaultAsync(p => p.PromotionId == id && p.IsActive);
        }

        public async Task<Promotion> CreateAsync(Promotion entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Promotions.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Promotion?> UpdateAsync(Promotion entity)
        {
            var existing = await _context.Promotions.FindAsync(entity.PromotionId);
            if (existing == null) return null;

            existing.PromoCode = entity.PromoCode;
            existing.DiscountPercentage = entity.DiscountPercentage;
            existing.StartDate = entity.StartDate;
            existing.EndDate = entity.EndDate;
            existing.ApplicableTo = entity.ApplicableTo;
            existing.StationId = entity.StationId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Promotions.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
