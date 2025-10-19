using System;

namespace ElectricVehicleDealer.Common.DTOs.ScheduleDtos
{
    public class UpdateScheduleDto
    {
        public int? StationCarId { get; set; }
        public string? Status { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
