using System;

namespace ElectricVehicleDealer.Common.DTOs.RoleDtos
{
    public class CreateRoleDto
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
