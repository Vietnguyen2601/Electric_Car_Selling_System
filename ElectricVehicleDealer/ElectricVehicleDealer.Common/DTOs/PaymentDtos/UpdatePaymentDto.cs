using System;

namespace ElectricVehicleDealer.Common.DTOs.PaymentDtos
{
    public class UpdatePaymentDto
    {
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
