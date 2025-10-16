using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<AccountDto> Accounts { get; set; } = new List<AccountDto>();

        public async Task OnGetAsync()
        {
            Accounts = await _accountService.GetAllAccountsAsync();
        }
    }
}
