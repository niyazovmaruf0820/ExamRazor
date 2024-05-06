using Domain.DTOs.CustomerDto;
using Infrasructure.Services.CusromerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer
{
    [IgnoreAntiforgeryToken]
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService customerService;

        public CreateCustomerModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [BindProperty] public AddCustomerDto _CustomerDto { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await customerService.CreateCustomerAsync(_CustomerDto);
            
            return RedirectToPage("/Customer/GetCustomers");
        }
    }
}