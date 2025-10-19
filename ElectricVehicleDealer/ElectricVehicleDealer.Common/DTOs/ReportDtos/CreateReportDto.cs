using System;

namespace ElectricVehicleDealer.Common.DTOs.ReportDtos
{
    public class CreateReportDto
    {
        public string ReportType { get; set; } = string.Empty;
        public DateTime GeneratedDate { get; set; }
        public string Data { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
