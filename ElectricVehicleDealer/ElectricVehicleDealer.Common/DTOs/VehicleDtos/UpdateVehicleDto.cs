using System;

namespace ElectricVehicleDealer.Common.DTOs.VehicleDtos
{
    public class UpdateVehicleDto
    {
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
