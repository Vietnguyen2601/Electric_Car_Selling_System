using ElectricVehicleDealer.Common.DTOs.ScheduleDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class ScheduleMapper
    {
        public static ScheduleDto ToDto(this Schedule entity)
        {
            return new ScheduleDto
            {
                ScheduleId = entity.ScheduleId,
                CustomerId = entity.CustomerId,
                StationCarId = entity.StationCarId,
                Status = entity.Status,
                ScheduleTime = entity.ScheduleTime,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                CustomerUsername = entity.Customer?.Username,
                StationName = entity.StationCar?.Station?.StationName
            };
        }

        public static List<ScheduleDto> ToDtoList(this IEnumerable<Schedule> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Schedule ToEntity(this CreateScheduleDto dto)
        {
            return new Schedule
            {
                CustomerId = dto.CustomerId,
                StationCarId = dto.StationCarId,
                Status = dto.Status,
                ScheduleTime = dto.ScheduleTime,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdateScheduleDto dto, Schedule entity)
        {
            if (dto.StationCarId.HasValue) entity.StationCarId = dto.StationCarId.Value;
            if (dto.Status is not null) entity.Status = dto.Status;
            if (dto.ScheduleTime.HasValue) entity.ScheduleTime = dto.ScheduleTime.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
