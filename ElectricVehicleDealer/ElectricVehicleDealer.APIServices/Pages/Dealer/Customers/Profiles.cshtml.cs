using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.AccountDto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Dealer.Customers
{
    public class ProfilesModel : PageModel
    {
        private readonly IAccountService _accountService;

        public ProfilesModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IReadOnlyList<AccountDto> Accounts { get; private set; } = Array.Empty<AccountDto>();

        public int ActiveAccounts => Accounts.Count(account => account.IsActive);
        public int InactiveAccounts => Accounts.Count(account => !account.IsActive);

        public async Task OnGetAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            Accounts = (accounts ?? Enumerable.Empty<AccountDto>())
                .OrderByDescending(account => account.CreatedAt)
                .ToList();
        }
    }
}
