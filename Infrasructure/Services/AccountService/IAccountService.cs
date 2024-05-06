using Domain.DTOs.AccountDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrasructure.Services.AccountService;

public interface IAccountService
{
    Task<PagedResponse<List<GetAccountsDto>>> GetAccountsAsync(AccountFilter filter);
    Task<Response<GetAccountsDto>> GetAccountByIdAsync(int accountId);
    Task<Response<string>> CreateAccountAsync(AddAccountDto account);
    Task<Response<string>> UpdateAccountAsync(UpdateAccountDto account);
    Task<Response<bool>> RemoveAccountAsync(int accountId);
}
