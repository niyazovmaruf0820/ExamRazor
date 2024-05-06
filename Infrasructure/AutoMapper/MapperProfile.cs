using AutoMapper;
using Domain.DTOs.AccountDto;
using Domain.DTOs.CustomerDto;
using Domain.DTOs.TransactionDto;
using Domain.Entities;

namespace Infrasructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Customer, AddCustomerDto>().ReverseMap();
        CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
        CreateMap<Customer, GetCustomersDto>().ReverseMap();
        CreateMap<Account, AddAccountDto>().ReverseMap();
        CreateMap<Account, UpdateAccountDto>().ReverseMap();
        CreateMap<Account, GetAccountsDto>().ReverseMap();
        CreateMap<Transaction, AddTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
    }
}
