using ElectricVehicleDealer.Common.DTOs.AccountDto;
using ElectricVehicleDealer.DAL.Models;
using System.Collections.Generic;
using System.Linq;


namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class AccountMapper
    {
        public static AccountDto ToDTO(Account entity)
        {
            return new AccountDto
            {
                AccountId = entity.AccountId,
                Username = entity.Username,
                Password = entity.Password,
                Email = entity.Email,
                ContactNumber = entity.ContactNumber,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Roles = entity.AccountRoles?
                    .Where(ar => ar.IsActive && ar.Role != null)
                    .Select(ar => ar.Role!.RoleName)
                    .ToList() ?? new List<string>(),
                RoleIds = entity.AccountRoles?
                    .Where(ar => ar.IsActive)
                    .Select(ar => ar.RoleId)
                    .ToList() ?? new List<int>()
            };
        }

        public static Account ToEntity(CreateAccountDto dto)
        {
            return new Account
            {
                Username = dto.Username,
                Password = dto.Password, // giữ nguyên
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
        }

        public static void UpdateEntity(Account entity, UpdateAccountDto dto)
        {
            if (dto.Password != null) entity.Password = dto.Password;
            if (dto.Email != null) entity.Email = dto.Email;
            if (dto.ContactNumber != null) entity.ContactNumber = dto.ContactNumber;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;

            entity.UpdatedAt = DateTime.Now;
        }
    }
}
