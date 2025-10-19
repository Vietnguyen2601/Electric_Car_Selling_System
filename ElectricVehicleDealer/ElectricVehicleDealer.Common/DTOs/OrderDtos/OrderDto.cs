using System;

namespace ElectricVehicleDealer.Common.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StationCarId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? PromotionId { get; set; }
        public int StaffId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? CustomerUsername { get; set; }
        public string? StaffUsername { get; set; }
        public string? StationName { get; set; }
    }
}
