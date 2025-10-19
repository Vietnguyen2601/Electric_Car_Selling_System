using ElectricVehicleDealer.Common.DTOs.ReportDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class ReportMapper
    {
        public static ReportDto ToDto(this Report entity)
        {
            return new ReportDto
            {
                ReportId = entity.ReportId,
                ReportType = entity.ReportType,
                GeneratedDate = entity.GeneratedDate,
                Data = entity.Data,
                AccountId = entity.AccountId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                AccountUsername = entity.Account?.Username
            };
        }

        public static List<ReportDto> ToDtoList(this IEnumerable<Report> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Report ToEntity(this CreateReportDto dto)
        {
            return new Report
            {
                ReportType = dto.ReportType,
                GeneratedDate = dto.GeneratedDate,
                Data = dto.Data,
                AccountId = dto.AccountId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdateReportDto dto, Report entity)
        {
            if (dto.ReportType is not null) entity.ReportType = dto.ReportType;
            if (dto.GeneratedDate.HasValue) entity.GeneratedDate = dto.GeneratedDate.Value;
            if (dto.Data is not null) entity.Data = dto.Data;
            if (dto.AccountId.HasValue) entity.AccountId = dto.AccountId.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
