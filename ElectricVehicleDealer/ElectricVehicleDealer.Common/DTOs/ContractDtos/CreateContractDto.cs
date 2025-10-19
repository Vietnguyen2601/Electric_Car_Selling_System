using System;

namespace ElectricVehicleDealer.Common.DTOs.ContractDtos
{
    public class CreateContractDto
    {
        public int OrderId { get; set; }
        public DateTime ContractDate { get; set; }
        public string Terms { get; set; } = string.Empty;
        public string? Signature { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
