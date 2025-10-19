using ElectricVehicleDealer.Common.DTOs.RoleDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Role entity)
        {
            return new RoleDto
            {
                RoleId = entity.RoleId,
                RoleName = entity.RoleName,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }

        public static List<RoleDto> ToDtoList(this IEnumerable<Role> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Role ToEntity(this CreateRoleDto dto)
        {
            return new Role
            {
                RoleName = dto.RoleName,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdateRoleDto dto, Role entity)
        {
            if (dto.RoleName is not null) entity.RoleName = dto.RoleName;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
