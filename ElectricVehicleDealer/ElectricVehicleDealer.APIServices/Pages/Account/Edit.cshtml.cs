using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.Common.DTOs.AccountDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricVehicleDealer.Presentation.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public EditModel(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public AccountDto? Account { get; set; }

        [BindProperty]
        public UpdateAccountDto Input { get; set; } = new();

        public List<SelectListItem> RoleOptions { get; set; } = new();

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
            Input.RoleIds = Account.RoleIds ?? new List<int>();

            await LoadRoleOptionsAsync(Input.RoleIds);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Account = await _accountService.GetAccountByIdAsync(id);
                await LoadRoleOptionsAsync(Input.RoleIds);
                return Page();
            }

            var success = await _accountService.UpdateAccountAsync(id, Input);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }

        private async Task LoadRoleOptionsAsync(IEnumerable<int> selectedRoleIds)
        {
            var roles = await _roleService.GetAllAsync();
            var selected = selectedRoleIds?.ToHashSet() ?? new HashSet<int>();

            RoleOptions = roles
                .Select(role => new SelectListItem
                {
                    Value = role.RoleId.ToString(),
                    Text = role.RoleName,
                    Selected = selected.Contains(role.RoleId)
                })
                .ToList();
        }
    }
}
