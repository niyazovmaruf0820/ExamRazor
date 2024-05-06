using Domain.DTOs.AccountDto;
using Infrasructure.Services.AccountService;
using Infrasructure.Services.CusromerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account
{
    public class UpdateAccountModel : PageModel
    {
        private readonly IAccountService _AccountService;

        public UpdateAccountModel(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }

        [BindProperty]
        public UpdateAccountDto _Account { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            _Account.Id = id; 
            await _AccountService.UpdateAccountAsync(_Account);

            return RedirectToPage("/Account/GetAccounts");
        }
    }
}