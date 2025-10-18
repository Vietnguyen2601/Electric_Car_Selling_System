using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Common.DTOs.StationDtos
{
    public class StationCarDTO
    {
        public int StationCarId { get; set; }
        public int VehicleId { get; set; }
        public int StationId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? StationName { get; set; }
        public string? Location { get; set; }
        public StationDTO Station { get; set; }
        public string? VehicleModel { get; set; }
       
    }
}
