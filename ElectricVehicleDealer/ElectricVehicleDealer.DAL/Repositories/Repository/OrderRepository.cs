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
    public class OrderRepository : IOrderRepository
    {
        private readonly ElectricVehicleDContext _context;

        public OrderRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Promotion)
                .Include(o => o.StationCar)
                    .ThenInclude(sc => sc.Station)
                .Include(o => o.StationCar)
                    .ThenInclude(sc => sc.Vehicle)
                .Where(o => o.IsActive)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Promotion)
                .Include(o => o.StationCar)
                    .ThenInclude(sc => sc.Station)
                .Include(o => o.StationCar)
                    .ThenInclude(sc => sc.Vehicle)
                .FirstOrDefaultAsync(o => o.OrderId == id && o.IsActive);
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Order?> UpdateAsync(Order entity)
        {
            var existing = await _context.Orders.FindAsync(entity.OrderId);
            if (existing == null) return null;

            existing.CustomerId = entity.CustomerId;
            existing.StationCarId = entity.StationCarId;
            existing.OrderDate = entity.OrderDate;
            existing.TotalPrice = entity.TotalPrice;
            existing.Status = entity.Status;
            existing.PromotionId = entity.PromotionId;
            existing.StaffId = entity.StaffId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Orders.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.Orders.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
