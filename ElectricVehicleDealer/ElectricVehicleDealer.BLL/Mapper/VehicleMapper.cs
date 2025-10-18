using ElectricVehicleDealer.Common.DTOs.VehicleDtos;
using ElectricVehicleDealer.DAL.Models;


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

    }
}
