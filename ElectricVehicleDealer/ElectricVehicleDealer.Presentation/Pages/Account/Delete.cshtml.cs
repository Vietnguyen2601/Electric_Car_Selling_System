using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.AccountDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DeleteModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public AccountDto? Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Account = await _accountService.GetAccountByIdAsync(id);
            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _accountService.DeleteAccountAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
