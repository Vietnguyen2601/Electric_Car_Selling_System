using ElectricVehicleDealer.Common.DTOs.ContractDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllAsync();
        Task<ContractDto?> GetByIdAsync(int id);
        Task<ContractDto> CreateAsync(CreateContractDto dto);
        Task<bool> UpdateAsync(int id, UpdateContractDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
