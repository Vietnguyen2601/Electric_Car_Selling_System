using System;

namespace ElectricVehicleDealer.Common.DTOs.VehicleDtos
{
    public class CreateVehicleDto
    {
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
