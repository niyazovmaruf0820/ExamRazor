using Infrasructure.AutoMapper;
using Infrasructure.Data;
using Infrasructure.Services.AccountService;
using Infrasructure.Services.CusromerService;
using Infrasructure.Services.TransactionService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


var connection = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<DataContext>(x=>x.UseNpgsql(connection));

builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));
// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();