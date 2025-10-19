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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ElectricVehicleDContext _context;

        public PaymentRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.PaymentId == id && p.IsActive);
        }

        public async Task<Payment> CreateAsync(Payment entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Payments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment?> UpdateAsync(Payment entity)
        {
            var existing = await _context.Payments.FindAsync(entity.PaymentId);
            if (existing == null) return null;

            existing.OrderId = entity.OrderId;
            existing.Amount = entity.Amount;
            existing.PaymentDate = entity.PaymentDate;
            existing.PaymentMethod = entity.PaymentMethod;
            existing.Status = entity.Status;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Payments.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Payments.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.Payments.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
