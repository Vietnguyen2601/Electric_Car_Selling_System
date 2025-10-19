using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs.AccountDto;
using ElectricVehicleDealer.DAL.Models;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using System.Collections.Generic;
using System.Linq;


namespace ElectricVehicleDealer.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository)
        {
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var entities = await _accountRepository.GetAllAccountsAsync();
            return entities.Select(AccountMapper.ToDTO);
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int accountId)
        {
            var entity = await _accountRepository.GetAccountByIdAsync(accountId);
            return entity != null ? AccountMapper.ToDTO(entity) : null;
        }

        public async Task AddAccountAsync(CreateAccountDto dto)
        {
            var entity = AccountMapper.ToEntity(dto);
            await _accountRepository.AddAccountAsync(entity);

            await SyncAccountRolesAsync(entity.AccountId, dto.RoleIds);
        }

        public async Task<bool> UpdateAccountAsync(int accountId, UpdateAccountDto dto)
        {
            var existing = await _accountRepository.GetAccountByIdAsync(accountId);
            if (existing == null)
            {
                return false;
            }

            AccountMapper.UpdateEntity(existing, dto);
            await _accountRepository.UpdateAccountAsync(existing);

            await SyncAccountRolesAsync(accountId, dto.RoleIds);
            return true;
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var existing = await _accountRepository.GetAccountByIdAsync(accountId);
            if (existing == null)
            {
                return false;
            }

            await _accountRepository.DeleteAccountAsync(accountId);
            return true;
        }

        private async Task SyncAccountRolesAsync(int accountId, IEnumerable<int> desiredRoleIds)
        {
            var desired = desiredRoleIds?.Where(id => id > 0).Distinct().ToList() ?? new List<int>();
            var currentAssignments = await _accountRoleRepository.GetByAccountIdAsync(accountId);

            var toDeactivate = currentAssignments
                .Where(ar => ar.IsActive && !desired.Contains(ar.RoleId))
                .ToList();

            foreach (var assignment in toDeactivate)
            {
                assignment.IsActive = false;
                await _accountRoleRepository.UpdateAsync(assignment);
            }

            foreach (var roleId in desired)
            {
                var existing = currentAssignments.FirstOrDefault(ar => ar.RoleId == roleId);
                if (existing == null)
                {
                    await _accountRoleRepository.CreateAsync(new AccountRole
                    {
                        AccountId = accountId,
                        RoleId = roleId,
                        IsActive = true
                    });
                }
                else if (!existing.IsActive)
                {
                    existing.IsActive = true;
                    await _accountRoleRepository.UpdateAsync(existing);
                }
            }
        }
    }
}
