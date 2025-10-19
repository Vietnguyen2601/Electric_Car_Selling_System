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
    public class ContractRepository : IContractRepository
    {
        private readonly ElectricVehicleDContext _context;

        public ContractRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            return await _context.Contracts
                .Include(c => c.Order)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await _context.Contracts
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.ContractId == id && c.IsActive);
        }

        public async Task<Contract> CreateAsync(Contract entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Contracts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Contract?> UpdateAsync(Contract entity)
        {
            var existing = await _context.Contracts.FindAsync(entity.ContractId);
            if (existing == null) return null;

            existing.OrderId = entity.OrderId;
            existing.ContractDate = entity.ContractDate;
            existing.Terms = entity.Terms;
            existing.Signature = entity.Signature;
            existing.Status = entity.Status;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Contracts.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
