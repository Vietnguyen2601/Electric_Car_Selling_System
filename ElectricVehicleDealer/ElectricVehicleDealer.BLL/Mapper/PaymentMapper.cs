using ElectricVehicleDealer.Common.DTOs.PaymentDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class PaymentMapper
    {
        public static PaymentDto ToDto(this Payment entity)
        {
            return new PaymentDto
            {
                PaymentId = entity.PaymentId,
                OrderId = entity.OrderId,
                Amount = entity.Amount,
                PaymentDate = entity.PaymentDate,
                PaymentMethod = entity.PaymentMethod,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                OrderStatus = entity.Order?.Status
            };
        }

        public static List<PaymentDto> ToDtoList(this IEnumerable<Payment> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Payment ToEntity(this CreatePaymentDto dto)
        {
            return new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                PaymentMethod = dto.PaymentMethod,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                IsActive = dto.IsActive
            };
        }

        public static void MapToEntity(this UpdatePaymentDto dto, Payment entity)
        {
            if (dto.Amount.HasValue) entity.Amount = dto.Amount.Value;
            if (dto.PaymentDate.HasValue) entity.PaymentDate = dto.PaymentDate.Value;
            if (dto.PaymentMethod is not null) entity.PaymentMethod = dto.PaymentMethod;
            if (dto.Status is not null) entity.Status = dto.Status;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
