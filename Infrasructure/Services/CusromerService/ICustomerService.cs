using Domain.DTOs.CustomerDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrasructure.Services.CusromerService;

public interface ICustomerService
{
    Task<PagedResponse<List<GetCustomersDto>>> GetCustomersAsync(CustomerFilter filter);
    Task<Response<GetCustomersDto>> GetCustomerByIdAsync(int customerId);
    Task<Response<string>> CreateCustomerAsync(AddCustomerDto customer);
    Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDto customer);
    Task<Response<bool>> RemoveCustomerAsync(int customerId);

}