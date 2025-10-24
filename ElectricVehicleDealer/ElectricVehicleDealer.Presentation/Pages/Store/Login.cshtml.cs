using ElectricVehicleDealer.BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectricVehicleDealer.Presentation.Pages.Store
{
    using System.Collections.Generic;
    using System.Linq;

    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [TempData]
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            var account = await _accountService.AuthenticateAsync(Email, Password);

            if (account != null)
            {
                HttpContext.Session.SetInt32("AccountId", account.AccountId);
                HttpContext.Session.SetString("AccountName", account.Username);
                var roles = account.Roles?.Where(r => !string.IsNullOrWhiteSpace(r)).ToList() ?? new List<string>();
                HttpContext.Session.SetString("AccountRoles", string.Join(',', roles));

                if (!string.IsNullOrEmpty(returnUrl))
                    return LocalRedirect(returnUrl);

                return RedirectToPage("/Homepage/Home");
            }

            ErrorMessage = "Sai email hoặc mật khẩu.";
            return Page();
        }
    }
}
