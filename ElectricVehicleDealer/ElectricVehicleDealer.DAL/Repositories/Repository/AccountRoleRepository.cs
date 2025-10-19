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
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly ElectricVehicleDContext _context;

        public AccountRoleRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<AccountRole>> GetAllAsync()
        {
            return await _context.AccountRoles
                .Include(ar => ar.Role)
                .Where(ar => ar.IsActive)
                .ToListAsync();
        }

        public async Task<AccountRole?> GetByIdAsync(int id)
        {
            return await _context.AccountRoles
                .Include(ar => ar.Role)
                .FirstOrDefaultAsync(ar => ar.AccountRoleId == id && ar.IsActive);
        }

        public async Task<AccountRole> CreateAsync(AccountRole entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.AccountRoles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AccountRole?> UpdateAsync(AccountRole entity)
        {
            var existing = await _context.AccountRoles.FindAsync(entity.AccountRoleId);
            if (existing == null) return null;

            existing.AccountId = entity.AccountId;
            existing.RoleId = entity.RoleId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.AccountRoles.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
