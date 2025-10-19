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
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public CreateModel(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        [BindProperty]
        public CreateAccountDto Input { get; set; } = new();

        public List<SelectListItem> RoleOptions { get; set; } = new();

        public async Task OnGetAsync()
        {
            await LoadRoleOptionsAsync(Input.RoleIds);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadRoleOptionsAsync(Input.RoleIds);
                return Page();
            }

            await _accountService.AddAccountAsync(Input);
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
