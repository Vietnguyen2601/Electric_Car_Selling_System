using System;

namespace ElectricVehicleDealer.Common.DTOs.FeedbackDtos
{
    public class UpdateFeedbackDto
    {
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
