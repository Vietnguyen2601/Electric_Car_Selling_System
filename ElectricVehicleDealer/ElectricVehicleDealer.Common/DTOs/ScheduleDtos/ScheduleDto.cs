using System;

namespace ElectricVehicleDealer.Common.DTOs.ScheduleDtos
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public int CustomerId { get; set; }
        public int StationCarId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime ScheduleTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? CustomerUsername { get; set; }
        public string? StationName { get; set; }
        public string? StationLocation { get; set; }
        public string? VehicleModel { get; set; }
    }
}
