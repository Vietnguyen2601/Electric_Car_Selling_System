using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IStationRepository
    {
        Task<List<Station>> GetAllAsync();
        Task<Station?> GetByIdAsync(int id);
        Task<Station> CreateAsync(Station station);
        Task<Station?> UpdateAsync(Station station);
        Task<bool> DeleteAsync(int id);
    }
}
