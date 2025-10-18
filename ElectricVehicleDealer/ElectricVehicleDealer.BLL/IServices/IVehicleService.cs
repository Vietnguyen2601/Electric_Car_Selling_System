using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAllAsync();
        Task<VehicleDTO?> GetByIdAsync(int id);
    }
}
