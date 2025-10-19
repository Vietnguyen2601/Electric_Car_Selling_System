using ElectricVehicleDealer.Common.DTOs.StaffRevenueDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class StaffRevenueMapper
    {
        public static StaffRevenueDto ToDto(this StaffRevenue entity)
        {
            return new StaffRevenueDto
            {
                StaffRevenueId = entity.StaffRevenueId,
                StaffId = entity.StaffId,
                RevenueDate = entity.RevenueDate,
                TotalRevenue = entity.TotalRevenue,
                Commission = entity.Commission,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                StaffUsername = entity.Staff?.Username
            };
        }

        public static List<StaffRevenueDto> ToDtoList(this IEnumerable<StaffRevenue> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static StaffRevenue ToEntity(this CreateStaffRevenueDto dto)
        {
            return new StaffRevenue
            {
                StaffId = dto.StaffId,
                RevenueDate = dto.RevenueDate,
                TotalRevenue = dto.TotalRevenue,
                Commission = dto.Commission,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdateStaffRevenueDto dto, StaffRevenue entity)
        {
            if (dto.RevenueDate.HasValue) entity.RevenueDate = dto.RevenueDate.Value;
            if (dto.TotalRevenue.HasValue) entity.TotalRevenue = dto.TotalRevenue.Value;
            if (dto.Commission.HasValue) entity.Commission = dto.Commission.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
