using ElectricVehicleDealer.Common.DTOs.StationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IStationCarService
    {
        Task<IEnumerable<StationCarDTO>> GetAllAsync();
        Task<StationCarDTO?> GetByIdAsync(int id);
        Task<IEnumerable<StationCarDTO>> GetByVehicleIdAsync(int vehicleId);
        Task<StationCarDTO> CreateAsync(StationCarDTO dto);
        Task<bool> UpdateAsync(int id, StationCarDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
