using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Mapper;
using ElectricVehicleDealer.Common.DTOs;
using ElectricVehicleDealer.DAL.Repositories.IRepository;


namespace ElectricVehicleDealer.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
        }

        public async Task UpdateAccountAsync(int accountId, UpdateAccountDto dto)
        {
            var existing = await _accountRepository.GetAccountByIdAsync(accountId);
            if (existing != null)
            {
                AccountMapper.UpdateEntity(existing, dto);
                await _accountRepository.UpdateAccountAsync(existing);
            }
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepository.DeleteAccountAsync(accountId);
        }
    }
}
