using System;

namespace ElectricVehicleDealer.Common.DTOs.OrderDtos
{
    public class UpdateOrderDto
    {
        public int? StationCarId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public int? PromotionId { get; set; }
        public int? StaffId { get; set; }
        public bool? IsActive { get; set; }
    }
}
