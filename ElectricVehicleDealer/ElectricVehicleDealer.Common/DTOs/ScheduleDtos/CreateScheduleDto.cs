using System;

namespace ElectricVehicleDealer.Common.DTOs.ScheduleDtos
{
    public class CreateScheduleDto
    {
        public int CustomerId { get; set; }
        public int StationCarId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime ScheduleTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
