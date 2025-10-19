using System;

namespace ElectricVehicleDealer.Common.DTOs.PromotionDtos
{
    public class CreatePromotionDto
    {
        public string PromoCode { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ApplicableTo { get; set; }
        public int? StationId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
