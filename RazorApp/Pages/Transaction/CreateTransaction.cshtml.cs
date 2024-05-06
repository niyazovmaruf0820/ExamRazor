using Domain.DTOs.TransactionDto;
using Infrasructure.Services.CusromerService;
using Infrasructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction
{
    [IgnoreAntiforgeryToken]
    public class CreateTransactionModel : PageModel
    {
        private readonly ITransactionService _TransactionService;

        public CreateTransactionModel(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }

        [BindProperty] public AddTransactionDto TransactionDto { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _TransactionService.CreateTransactionAsync(TransactionDto);
            
            return RedirectToPage("/Transaction/GetTransactions");
        }
    }
}