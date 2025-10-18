using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Common.DTOs.VehicleDtos
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string? Color { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        //Sau này có thể sử dụng
        //public string? DisplayPrice
        //{
        //    get => $"{Price:N0} ₫";
        //}

        //public string? StatusText
        //{
        //    get => IsActive ? "Đang kinh doanh" : "Ngừng kinh doanh";
        //}

    }
}
