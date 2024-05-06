using System.Net;
using Domain.DTOs.AccountDto;
using Domain.Filters;
using Infrasructure.Services.AccountService;
using Infrasructure.Services.CusromerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account
{
    public class GetAccountsModel : PageModel
    {
        private readonly IAccountService _AccountService;

        public GetAccountsModel(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }

        [BindProperty(SupportsGet = true)]
        public AccountFilter Filter { get; set; }

        public List<GetAccountsDto> Accounts { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _AccountService.GetAccountsAsync(Filter);
                Accounts = response.Data;
                TotalPages = response.TotalPages;
                return Page();
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}