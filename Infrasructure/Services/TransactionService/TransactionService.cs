using AutoMapper;
using Domain.DTOs.TransactionDto;
using Domain.Filters;
using Domain.Responses;
using Infrasructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
using System.Transactions;

namespace Infrasructure.Services.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public TransactionService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<PagedResponse<List<GetTransactionsDto>>> GetTransactionsAsync(TransactionFilter filter)
    {
        try
        {
            var Transactions = _context.Transactions.AsQueryable();
            var result = await Transactions.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Transactions.CountAsync();

            var response = _mapper.Map<List<GetTransactionsDto>>(result);
            return new PagedResponse<List<GetTransactionsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTransactionsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetTransactionsDto>> GetTransactionByIdAsync(int TransactionId)
    {
        try
        {
            var existing = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == TransactionId);
            if (existing == null) return new Response<GetTransactionsDto>(HttpStatusCode.BadRequest, "Transaction not found");
            var Transaction = _mapper.Map<GetTransactionsDto>(existing);
            return new Response<GetTransactionsDto>(Transaction);
        }
        catch (Exception e)
        {
            return new Response<GetTransactionsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateTransactionAsync(AddTransactionDto Transaction)
    {
        try
        {
            var existing = await _context.Transactions.AnyAsync(x => x.FromAccountId == Transaction.FromAccountId);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Transaction already exists");
            var newTransaction = _mapper.Map<Domain.Entities.Transaction>(Transaction);
            await _context.Transactions.AddAsync(newTransaction);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }



    public async Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDto Transaction)
    {
        try
        {
            var existing = await _context.Transactions.AnyAsync(x => x.Id == Transaction.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Transaction not found");
            var newTransaction = _mapper.Map<Domain.Entities.Transaction>(Transaction);
            _context.Transactions.Update(newTransaction);
            await _context.SaveChangesAsync();
            return new Response<string>("Transaction successfully updated");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<bool>> RemoveTransactionAsync(int TransactionId)
    {
        try
        {
            var existing = await _context.Transactions.Where(x => x.Id == TransactionId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Transaction not found")
                : new Response<bool>(true);
        }
        catch (DbException e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
