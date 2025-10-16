using ElectricVehicleDealer.Common.DTOs;


namespace ElectricVehicleDealer.BLL.IServices
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
        Task<AccountDto?> GetAccountByIdAsync(int accountId);
        Task AddAccountAsync(CreateAccountDto dto);
        Task UpdateAccountAsync(int accountId, UpdateAccountDto dto);
        Task DeleteAccountAsync(int accountId);
    }
}
