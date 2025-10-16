using ElectricVehicleDealer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.DAL.Repositories.IRepository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);

    }
}
