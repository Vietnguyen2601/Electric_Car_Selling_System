using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Common.DTOs
{
    public class UpdateAccountDto
    {
        public int AccountId { get; set; }
        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? ContactNumber { get; set; }

        public bool? IsActive { get; set; }
        //public string? NewPassword { get; set; }

    }
}
