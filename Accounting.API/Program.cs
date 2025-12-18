using Accounting.Application.Interfaces;
using Accounting.Application.Services;
using Accounting.Infrastructure;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configure the DataContext
builder.Services.AddDbContext<AccountingDbContext>(options =>
{
    options.UseSqlServer(
                builder.Configuration.GetConnectionString("AccountingConnection"),
                sqlServerOptionsAction: sqlOptions => {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null
                    );
                }
            );
});
// Add services to the container.
builder.Services.AddControllers();

//Accounts
builder.Services.AddTransient<IAccountsService, AccountsService>();
builder.Services.AddTransient<IAccountsRepository, AccountsRepository>();
//Coins
builder.Services.AddTransient<ICoinsService, CoinsService>();
builder.Services.AddTransient<ICoinsRepository, CoinsRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Accounting API");
        }
    );
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "Accounting/{controller=Home}/{action=Index}/{id?}"
);

app.Run();