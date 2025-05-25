using eTicaret.CartWebAPI;
using eTicaret.CartWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMassTransit(x =>
//{
//    x.UsingRabbitMq((context, cfr) =>
//    {
//        cfr.Host("localhost", "/", h => { });
//    });
//});


builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseInMemoryDatabase("MyDb");
});

builder.Services.AddOpenApi();

builder.Services.AddCors();

builder.Services.AddServiceDiscovery(o => o.UseConsul());

builder.Services
    .AddHttpClient()
    .AddServiceDiscovery();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod()
.SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

app.MapCartEndpoint(builder.Configuration);

app.Run();
