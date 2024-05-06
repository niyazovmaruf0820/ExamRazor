using System.Threading.Tasks;
using Domain.DTOs.TransactionDto;
using Infrasructure.Services.CusromerService;
using Infrasructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction
{
    public class GetTransactionByIdModel : PageModel
    {
        private readonly ITransactionService _TransactionService;

        public GetTransactionByIdModel(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }

        public GetTransactionsDto Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var transaction = await _TransactionService.GetTransactionByIdAsync(id);
            Transaction = transaction.Data;
            if (Transaction == null)
            {
                return NotFound(); 
            }

            return Page();
        }
    }
}