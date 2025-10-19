using System;

namespace ElectricVehicleDealer.Common.DTOs.FeedbackDtos
{
    public class CreateFeedbackDto
    {
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime FeedbackDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
