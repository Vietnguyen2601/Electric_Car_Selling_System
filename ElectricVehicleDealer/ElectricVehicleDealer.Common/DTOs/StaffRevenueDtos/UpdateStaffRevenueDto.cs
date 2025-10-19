using System;

namespace ElectricVehicleDealer.Common.DTOs.StaffRevenueDtos
{
    public class UpdateStaffRevenueDto
    {
        public DateTime? RevenueDate { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? Commission { get; set; }
        public bool? IsActive { get; set; }
    }
}
