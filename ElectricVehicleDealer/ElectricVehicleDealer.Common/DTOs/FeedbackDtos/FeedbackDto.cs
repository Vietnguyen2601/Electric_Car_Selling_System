using System;

namespace ElectricVehicleDealer.Common.DTOs.FeedbackDtos
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime FeedbackDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? CustomerUsername { get; set; }
        public string? VehicleModel { get; set; }
    }
}
