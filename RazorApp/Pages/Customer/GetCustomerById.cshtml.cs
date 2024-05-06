using System.Threading.Tasks;
using Domain.DTOs.CustomerDto;
using Infrasructure.Services.CusromerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer
{
    public class GetCustomerByIdModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByIdModel(ICustomerService CustomerService)
        {
            _customerService = CustomerService;
        }

        public GetCustomersDto Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            Customer = customer.Data;
            if (customer == null)
            {
                return NotFound(); 
            }

            return Page();
        }
    }
}