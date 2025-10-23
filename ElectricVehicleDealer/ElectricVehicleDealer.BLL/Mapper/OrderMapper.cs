using ElectricVehicleDealer.Common.DTOs.OrderDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order entity)
        {
            return new OrderDto
            {
                OrderId = entity.OrderId,
                CustomerId = entity.CustomerId,
                StationCarId = entity.StationCarId,
                OrderDate = entity.OrderDate,
                TotalPrice = entity.TotalPrice,
                Status = entity.Status,
                PromotionId = entity.PromotionId,
                StaffId = entity.StaffId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                CustomerUsername = entity.Customer?.Username,
                StaffUsername = entity.Staff?.Username,
                StationName = entity.StationCar?.Station?.StationName,
                VehicleModel = entity.StationCar?.Vehicle?.Model
            };
        }

        public static List<OrderDto> ToDtoList(this IEnumerable<Order> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Order ToEntity(this CreateOrderDto dto)
        {
            return new Order
            {
                CustomerId = dto.CustomerId,
                StationCarId = dto.StationCarId,
                OrderDate = dto.OrderDate,
                TotalPrice = dto.TotalPrice,
                Status = dto.Status,
                PromotionId = dto.PromotionId,
                StaffId = dto.StaffId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToEntity(this UpdateOrderDto dto, Order entity)
        {
            if (dto.StationCarId.HasValue) entity.StationCarId = dto.StationCarId.Value;
            if (dto.OrderDate.HasValue) entity.OrderDate = dto.OrderDate.Value;
            if (dto.TotalPrice.HasValue) entity.TotalPrice = dto.TotalPrice.Value;
            if (dto.Status is not null) entity.Status = dto.Status;
            if (dto.PromotionId.HasValue) entity.PromotionId = dto.PromotionId;
            if (dto.StaffId.HasValue) entity.StaffId = dto.StaffId.Value;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
