using AutoMapper;
using Domain.DTOs.CustomerDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrasructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrasructure.Services.CusromerService;


public class CustomerService : ICustomerService
{


    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CustomerService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<PagedResponse<List<GetCustomersDto>>> GetCustomersAsync(CustomerFilter filter)
    {
        try
        {
            var customers = _context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                customers = customers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            var result = await customers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await customers.CountAsync();

            var response = _mapper.Map<List<GetCustomersDto>>(result);
            return new PagedResponse<List<GetCustomersDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCustomersDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetCustomersDto>> GetCustomerByIdAsync(int customerId)
    {
        try
        {
            var existing = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            if (existing == null) return new Response<GetCustomersDto>(HttpStatusCode.BadRequest, "Customer not found");
            var Customer = _mapper.Map<GetCustomersDto>(existing);
            return new Response<GetCustomersDto>(Customer);
        }
        catch (Exception e)
        {
            return new Response<GetCustomersDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> CreateCustomerAsync(AddCustomerDto customer)
    {
        try
        {
            var existing = await _context.Customers.AnyAsync(x => x.Name == customer.Name);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Customer already exists");
            var newCustomer = _mapper.Map<Customer>(customer);
            await _context.Customers.AddAsync(newCustomer);
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



    public async Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDto customer)
    {
        try
        {
            var existing = await _context.Customers.AnyAsync(x => x.Id == customer.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Customer not found");
            var newCustomer = _mapper.Map<Customer>(customer);
            _context.Customers.Update(newCustomer);
            await _context.SaveChangesAsync();
            return new Response<string>("Customer successfully updated");
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


    public async Task<Response<bool>> RemoveCustomerAsync(int customerId)
    {
        try
        {
            var existing = await _context.Customers.Where(x => x.Id == customerId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Customer not found")
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