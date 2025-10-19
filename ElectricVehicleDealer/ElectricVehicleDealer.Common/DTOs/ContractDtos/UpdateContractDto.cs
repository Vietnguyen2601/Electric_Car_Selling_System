using System;

namespace ElectricVehicleDealer.Common.DTOs.ContractDtos
{
    public class UpdateContractDto
    {
        public DateTime? ContractDate { get; set; }
        public string? Terms { get; set; }
        public string? Signature { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
