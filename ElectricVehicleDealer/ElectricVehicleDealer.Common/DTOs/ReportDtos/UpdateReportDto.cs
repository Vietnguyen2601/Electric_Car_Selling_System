using System;

namespace ElectricVehicleDealer.Common.DTOs.ReportDtos
{
    public class UpdateReportDto
    {
        public string? ReportType { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public string? Data { get; set; }
        public int? AccountId { get; set; }
        public bool? IsActive { get; set; }
    }
}
