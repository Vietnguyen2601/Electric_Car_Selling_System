using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Common.DTOs.AccountDto
{
    public class AccountDto
    {
        public int AccountId { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? ContactNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        // Nếu cần hiển thị vai trò
        public List<string>? Roles { get; set; }
    }
}
