using ElectricVehicleDealer.Common.DTOs.PromotionDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class PromotionMapper
    {
        public static PromotionDto ToDto(this Promotion entity)
        {
            return new PromotionDto
            {
                PromotionId = entity.PromotionId,
                PromoCode = entity.PromoCode,
                DiscountPercentage = entity.DiscountPercentage,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ApplicableTo = entity.ApplicableTo,
                StationId = entity.StationId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                StationName = entity.Station?.StationName
            };
        }

        public static List<PromotionDto> ToDtoList(this IEnumerable<Promotion> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Promotion ToEntity(this CreatePromotionDto dto)
        {
            return new Promotion
            {
                PromoCode = dto.PromoCode,
                DiscountPercentage = dto.DiscountPercentage,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ApplicableTo = dto.ApplicableTo,
                StationId = dto.StationId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdatePromotionDto dto, Promotion entity)
        {
            if (dto.PromoCode is not null) entity.PromoCode = dto.PromoCode;
            if (dto.DiscountPercentage.HasValue) entity.DiscountPercentage = dto.DiscountPercentage.Value;
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate.Value;
            if (dto.ApplicableTo is not null) entity.ApplicableTo = dto.ApplicableTo;
            if (dto.StationId.HasValue) entity.StationId = dto.StationId;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
