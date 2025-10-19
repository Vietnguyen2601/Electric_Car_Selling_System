using System;

namespace ElectricVehicleDealer.Common.DTOs.AccountRoleDtos
{
    public class CreateAccountRoleDto
    {
        public int AccountId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
