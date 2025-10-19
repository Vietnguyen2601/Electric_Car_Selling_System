using System;

namespace ElectricVehicleDealer.Common.DTOs.AccountRoleDtos
{
    public class UpdateAccountRoleDto
    {
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}
