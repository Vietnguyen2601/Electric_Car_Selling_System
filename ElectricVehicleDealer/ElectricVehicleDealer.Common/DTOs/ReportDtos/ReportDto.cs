using System;

namespace ElectricVehicleDealer.Common.DTOs.ReportDtos
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public DateTime GeneratedDate { get; set; }
        public string Data { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? AccountUsername { get; set; }
    }
}
