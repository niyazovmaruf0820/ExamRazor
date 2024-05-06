using Domain.DTOs.AccountDto;
using Infrasructure.Services.AccountService;
using Infrasructure.Services.CusromerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account
{
    [IgnoreAntiforgeryToken]
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _AccountService;

        public CreateAccountModel(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }

        [BindProperty] public AddAccountDto AccountDto { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _AccountService.CreateAccountAsync(AccountDto);
            
            return RedirectToPage("/Account/GetAccounts");
        }
    }
}