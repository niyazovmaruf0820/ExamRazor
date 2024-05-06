using Domain.DTOs.TransactionDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrasructure.Services.TransactionService;

public interface ITransactionService
{
    Task<PagedResponse<List<GetTransactionsDto>>> GetTransactionsAsync(TransactionFilter filter);
    Task<Response<GetTransactionsDto>> GetTransactionByIdAsync(int transactionId);
    Task<Response<string>> CreateTransactionAsync(AddTransactionDto transaction);
    Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDto transaction);
    Task<Response<bool>> RemoveTransactionAsync(int transactionId);
}
