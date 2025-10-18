using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IStationCarRepository
    {
        Task<IEnumerable<StationCar>> GetAllAsync();
        Task<StationCar?> GetByIdAsync(int id);
        Task<IEnumerable<StationCar>> GetByVehicleIdAsync(int vehicleId);
        Task AddAsync(StationCar entity);
        Task UpdateAsync(StationCar entity);
        Task DeleteAsync(StationCar entity);
    }
}
