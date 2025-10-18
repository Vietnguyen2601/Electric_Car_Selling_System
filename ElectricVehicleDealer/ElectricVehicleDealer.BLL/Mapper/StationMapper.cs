using ElectricVehicleDealer.Common.DTOs.StationDtos;
using ElectricVehicleDealer.DAL.Models;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class StationMapper
    {
        public static StationDTO ToDTO(this Station entity)
        {
            if (entity == null) return null!;

            return new StationDTO
            {
                StationId = entity.StationId,
                StationName = entity.StationName,
                Location = entity.Location,
                ContactNumber = entity.ContactNumber,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }

        public static List<StationDTO> ToDTOList(this IEnumerable<Station> entities)
        {
            return entities.Select(s => s.ToDTO()).ToList();
        }

        public static Station ToEntity(this StationDTO dto)
        {
            if (dto == null) return null!;

            return new Station
            {
                StationId = dto.StationId,
                StationName = dto.StationName,
                Location = dto.Location,
                ContactNumber = dto.ContactNumber,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                IsActive = dto.IsActive
            };
        }
    }
}
