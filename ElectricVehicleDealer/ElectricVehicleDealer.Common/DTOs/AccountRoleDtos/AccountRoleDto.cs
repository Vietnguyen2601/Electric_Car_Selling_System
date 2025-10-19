using System;

namespace ElectricVehicleDealer.Common.DTOs.AccountRoleDtos
{
    public class AccountRoleDto
    {
        public int AccountRoleId { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? RoleName { get; set; }
    }
}
