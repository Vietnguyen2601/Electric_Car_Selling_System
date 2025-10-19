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
    public class RoleRepository : IRoleRepository
    {
        private readonly ElectricVehicleDContext _context;

        public RoleRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Where(r => r.IsActive)
                .ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == id && r.IsActive);
        }

        public async Task<Role> CreateAsync(Role entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Role?> UpdateAsync(Role entity)
        {
            var existing = await _context.Roles.FindAsync(entity.RoleId);
            if (existing == null) return null;

            existing.RoleName = entity.RoleName;
            existing.IsActive = entity.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Roles.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Roles.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.Roles.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
