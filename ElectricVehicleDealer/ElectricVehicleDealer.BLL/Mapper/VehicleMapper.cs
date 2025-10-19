using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class VehicleMapper
    {
        public static VehicleDTO ToDTO(this Vehicle entity)
        {
            return new VehicleDTO
            {
                VehicleId = entity.VehicleId,
                Model = entity.Model,
                Type = entity.Type,
                Color = entity.Color,
                Price = entity.Price,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }

        public static List<VehicleDTO> ToDTOList(this IEnumerable<Vehicle> entities)
        {
            return entities.Select(v => v.ToDTO()).ToList();
        }

        public static Vehicle ToEntity(this CreateVehicleDto dto)
        {
            return new Vehicle
            {
                Model = dto.Model,
                Type = dto.Type,
                Color = dto.Color,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow,
                IsActive = dto.IsActive
            };
        }

        public static void MapToEntity(this UpdateVehicleDto dto, Vehicle entity)
        {
            if (dto.Model is not null) entity.Model = dto.Model;
            if (dto.Type is not null) entity.Type = dto.Type;
            if (dto.Color is not null) entity.Color = dto.Color;
            if (dto.Price.HasValue) entity.Price = dto.Price.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }

    }
}
