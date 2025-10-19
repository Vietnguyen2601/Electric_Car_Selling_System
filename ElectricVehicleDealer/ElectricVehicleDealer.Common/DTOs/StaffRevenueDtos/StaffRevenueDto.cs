using System;

namespace ElectricVehicleDealer.Common.DTOs.StaffRevenueDtos
{
    public class StaffRevenueDto
    {
        public int StaffRevenueId { get; set; }
        public int StaffId { get; set; }
        public DateTime RevenueDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal? Commission { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? StaffUsername { get; set; }
    }
}
