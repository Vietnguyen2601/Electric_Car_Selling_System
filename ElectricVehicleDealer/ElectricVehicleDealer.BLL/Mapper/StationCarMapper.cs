using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class StationCarMapper
    {
        public static StationCarDTO ToDTO(this StationCar entity)
        {
            return new StationCarDTO
            {
                StationCarId = entity.StationCarId,
                VehicleId = entity.VehicleId,
                StationId = entity.StationId,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                StationName = entity.Station?.StationName,
                VehicleModel = entity.Vehicle?.Model,
                Location = entity.Station?.Location
            };
        }

        public static List<StationCarDTO> ToDTOList(this IEnumerable<StationCar> entities)
        {
            return entities.Select(e => e.ToDTO()).ToList();
        }

        public static StationCar ToEntity(this StationCarDTO dto)
        {
            return new StationCar
            {
                StationCarId = dto.StationCarId,
                VehicleId = dto.VehicleId,
                StationId = dto.StationId,
                Quantity = dto.Quantity,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                IsActive = dto.IsActive
            };
        }
    }
}
