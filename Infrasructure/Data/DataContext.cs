using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrasructure.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options){}

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; }= null!;
    public DbSet<Transaction> Transactions { get; set; }= null!;

    protected override void OnModelCreating(ModelBuilder builder)
        => base.OnModelCreating(builder);
}