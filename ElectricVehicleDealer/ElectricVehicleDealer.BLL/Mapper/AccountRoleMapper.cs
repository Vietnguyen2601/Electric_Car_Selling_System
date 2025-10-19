using ElectricVehicleDealer.Common.DTOs.AccountRoleDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class AccountRoleMapper
    {
        public static AccountRoleDto ToDto(this AccountRole entity)
        {
            return new AccountRoleDto
            {
                AccountRoleId = entity.AccountRoleId,
                AccountId = entity.AccountId,
                RoleId = entity.RoleId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                RoleName = entity.Role?.RoleName
            };
        }

        public static List<AccountRoleDto> ToDtoList(this IEnumerable<AccountRole> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static AccountRole ToEntity(this CreateAccountRoleDto dto)
        {
            return new AccountRole
            {
                AccountId = dto.AccountId,
                RoleId = dto.RoleId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdateAccountRoleDto dto, AccountRole entity)
        {
            if (dto.RoleId.HasValue) entity.RoleId = dto.RoleId.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
