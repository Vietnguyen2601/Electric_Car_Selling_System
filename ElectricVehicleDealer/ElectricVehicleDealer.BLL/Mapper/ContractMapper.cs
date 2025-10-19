using ElectricVehicleDealer.Common.DTOs.ContractDtos;
using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricVehicleDealer.BLL.Mapper
{
    public static class ContractMapper
    {
        public static ContractDto ToDto(this Contract entity)
        {
            return new ContractDto
            {
                ContractId = entity.ContractId,
                OrderId = entity.OrderId,
                ContractDate = entity.ContractDate,
                Terms = entity.Terms,
                Signature = entity.Signature,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }

        public static List<ContractDto> ToDtoList(this IEnumerable<Contract> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }

        public static Contract ToEntity(this CreateContractDto dto)
        {
            return new Contract
            {
                OrderId = dto.OrderId,
                ContractDate = dto.ContractDate,
                Terms = dto.Terms,
                Signature = dto.Signature,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                IsActive = dto.IsActive
            };
        }

        public static void MapToEntity(this UpdateContractDto dto, Contract entity)
        {
            if (dto.ContractDate.HasValue) entity.ContractDate = dto.ContractDate.Value;
            if (dto.Terms is not null) entity.Terms = dto.Terms;
            if (dto.Signature is not null) entity.Signature = dto.Signature;
            if (dto.Status is not null) entity.Status = dto.Status;
            if (dto.IsActive.HasValue) entity.IsActive = dto.IsActive.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
