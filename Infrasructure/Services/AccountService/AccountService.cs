using AutoMapper;
using Domain.DTOs.AccountDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrasructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrasructure.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public AccountService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<PagedResponse<List<GetAccountsDto>>> GetAccountsAsync(AccountFilter filter)
    {
        try
        {
            var Accounts = _context.Accounts.AsQueryable();
            if (!string.IsNullOrEmpty(filter.AccountNumber))
                Accounts = Accounts.Where(x => x.AccountNumber == filter.AccountNumber);
            var result = await Accounts.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Accounts.CountAsync();

            var response = _mapper.Map<List<GetAccountsDto>>(result);
            return new PagedResponse<List<GetAccountsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetAccountsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetAccountsDto>> GetAccountByIdAsync(int AccountId)
    {
        try
        {
            var existing = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == AccountId);
            if (existing == null) return new Response<GetAccountsDto>(HttpStatusCode.BadRequest, "Account not found");
            var Account = _mapper.Map<GetAccountsDto>(existing);
            return new Response<GetAccountsDto>(Account);
        }
        catch (Exception e)
        {
            return new Response<GetAccountsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateAccountAsync(AddAccountDto Account)
    {
        try
        {
            var existing = await _context.Accounts.AnyAsync(x => x.AccountNumber == Account.AccountNumber);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Account already exists");
            var newAccount = _mapper.Map<Account>(Account);
            await _context.Accounts.AddAsync(newAccount);
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



    public async Task<Response<string>> UpdateAccountAsync(UpdateAccountDto Account)
    {
        try
        {
            var existing = await _context.Accounts.AnyAsync(x => x.Id == Account.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Account not found");
            var newAccount = _mapper.Map<Account>(Account);
            _context.Accounts.Update(newAccount);
            await _context.SaveChangesAsync();
            return new Response<string>("Account successfully updated");
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


    public async Task<Response<bool>> RemoveAccountAsync(int AccountId)
    {
        try
        {
            var existing = await _context.Accounts.Where(x => x.Id == AccountId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Account not found")
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
