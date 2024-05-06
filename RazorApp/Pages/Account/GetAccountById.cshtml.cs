using System.Threading.Tasks;
using Domain.DTOs.AccountDto;
using Infrasructure.Services.AccountService;
using Infrasructure.Services.CusromerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account
{
    public class GetAccountByIdModel : PageModel
    {
        private readonly IAccountService _AccountService;

        public GetAccountByIdModel(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }

        public GetAccountsDto Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var account = await _AccountService.GetAccountByIdAsync(id);
            Account = account.Data;
            if (Account == null)
            {
                return NotFound(); 
            }

            return Page();
        }
    }
}