using ElectricVehicleDealer.DAL.DBContext;
using ElectricVehicleDealer.DAL.Models;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;


namespace ElectricVehicleDealer.DAL.Repositories.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ElectricVehicleDContext _context;

        public AccountRepository(ElectricVehicleDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return _context.Accounts.ToList();
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Account?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password && a.IsActive == true);
        }
    }
}
