using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Common.DTOs.StationDtos
{
    public class StationDTO
    {
        public int StationId { get; set; }
        public string StationName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
