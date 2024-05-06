using System.Net;
using Domain.DTOs.TransactionDto;
using Domain.Filters;
using Infrasructure.Services.CusromerService;
using Infrasructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction
{
    public class GetTransactionsModel : PageModel
    {
        private readonly ITransactionService _TransactionService;

        public GetTransactionsModel(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }

        [BindProperty(SupportsGet = true)]
        public TransactionFilter Filter { get; set; }

        public List<GetTransactionsDto> Transactions { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _TransactionService.GetTransactionsAsync(Filter);
                Transactions = response.Data;
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