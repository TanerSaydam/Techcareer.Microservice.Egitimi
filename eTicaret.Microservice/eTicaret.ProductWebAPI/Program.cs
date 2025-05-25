using Bogus;
using eTicaret.ProductWebAPI;
using eTicaret.ProductWebAPI.Context;
using eTicaret.ProductWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")).UseSnakeCaseNamingConvention();
});

builder.Services.AddServiceDiscovery(u => u.UseConsul());

//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<UpdateStockConsumer>();
//    x.UsingRabbitMq((context, cfr) =>
//    {
//        cfr.Host("localhost", "/", h => { });
//        cfr.ReceiveEndpoint("product-stock-update", e =>
//        {
//            e.ConfigureConsumer<UpdateStockConsumer>(context);
//        });
//    });
//});

builder.Services.AddOpenApi();

builder.Services.AddCors();

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseExceptionHandler();

app.UseCors(x => x
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()
.SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

app.MapProductEndpoint();

using (var scoped = app.Services.CreateScope())
{
    var dbContext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

using (var scoped = app.Services.CreateScope())
{
    var dbContext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    List<Product> products = new();
    if (!dbContext.Products.Any())
    {
        for (int i = 0; i < 20; i++)
        {
            Faker faker = new();
            Product product = new()
            {
                Name = faker.Commerce.ProductName(),
                Price = faker.Commerce.Random.Decimal(10, 1000),
                Stock = faker.Commerce.Random.Int(1, 100)
            };
            products.Add(product);
        }

        dbContext.AddRange(products);
        dbContext.SaveChanges();
    }
}

app.Run();
