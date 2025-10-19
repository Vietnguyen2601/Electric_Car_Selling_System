using ElectricVehicleDealer.Common.DTOs.FeedbackDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class FeedbackMapper
    {
        public static FeedbackDto ToDto(this Feedback entity)
        {
            return new FeedbackDto
            {
                FeedbackId = entity.FeedbackId,
                CustomerId = entity.CustomerId,
                VehicleId = entity.VehicleId,
                Rating = entity.Rating,
                Comment = entity.Comment,
                FeedbackDate = entity.FeedbackDate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                CustomerUsername = entity.Customer?.Username,
                VehicleModel = entity.Vehicle?.Model
            };
        }

        public static List<FeedbackDto> ToDtoList(this IEnumerable<Feedback> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Feedback ToEntity(this CreateFeedbackDto dto)
        {
            return new Feedback
            {
                CustomerId = dto.CustomerId,
                VehicleId = dto.VehicleId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                FeedbackDate = dto.FeedbackDate,
                CreatedAt = DateTime.UtcNow,
                IsActive = dto.IsActive
            };
        }

        public static void MapToEntity(this UpdateFeedbackDto dto, Feedback entity)
        {
            if (dto.Rating.HasValue) entity.Rating = dto.Rating.Value;
            if (dto.Comment is not null) entity.Comment = dto.Comment;
            if (dto.FeedbackDate.HasValue) entity.FeedbackDate = dto.FeedbackDate.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
