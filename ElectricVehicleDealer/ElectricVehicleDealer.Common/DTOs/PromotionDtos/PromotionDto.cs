using System;

namespace ElectricVehicleDealer.Common.DTOs.PromotionDtos
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public string PromoCode { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ApplicableTo { get; set; }
        public int? StationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? StationName { get; set; }
    }
}
