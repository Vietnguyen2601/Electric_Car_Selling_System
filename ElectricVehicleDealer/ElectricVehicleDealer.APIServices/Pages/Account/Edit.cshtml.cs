using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.AccountDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public AccountDto? Account { get; set; }

        [BindProperty]
        public UpdateAccountDto Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Account = await _accountService.GetAccountByIdAsync(id);
            if (Account == null)
            {
                return NotFound();
            }

            Input.AccountId = Account.AccountId;
            Input.Email = Account.Email;
            Input.ContactNumber = Account.ContactNumber;
            Input.IsActive = Account.IsActive;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Account = await _accountService.GetAccountByIdAsync(id);
                return Page();
            }

            var success = await _accountService.UpdateAccountAsync(id, Input);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
