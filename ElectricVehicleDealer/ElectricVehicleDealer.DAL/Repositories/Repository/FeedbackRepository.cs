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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ElectricVehicleDContext _context;

        public FeedbackRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .Where(f => f.IsActive)
                .ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(f => f.FeedbackId == id && f.IsActive);
        }

        public async Task<Feedback> CreateAsync(Feedback entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Feedbacks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Feedback?> UpdateAsync(Feedback entity)
        {
            var existing = await _context.Feedbacks.FindAsync(entity.FeedbackId);
            if (existing == null) return null;

            existing.CustomerId = entity.CustomerId;
            existing.VehicleId = entity.VehicleId;
            existing.Rating = entity.Rating;
            existing.Comment = entity.Comment;
            existing.FeedbackDate = entity.FeedbackDate;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Feedbacks.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Feedbacks.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.Feedbacks.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
