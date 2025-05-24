using eTicaret.AuthWebAPI;
using eTicaret.AuthWebAPI.Context;
using eTicaret.AuthWebAPI.Dtos;
using eTicaret.AuthWebAPI.Models.Roles;
using eTicaret.AuthWebAPI.Models.Users;
using eTicaret.AuthWebAPI.Options;
using eTicaret.AuthWebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TS.Result;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("SqlServer")!;
    options.UseSqlServer(connectionString);
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services
    .AddIdentity<User, Role>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 1;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<JwtProvider>();

builder.Services.AddOpenApi();

builder.Services.AddCors();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/", () => "Hello World");

app.UseCors(x => x
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod()
.SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

app.MapPost("/login",
    async (
        LoginDto request,
        UserManager<User> userManager,
        JwtProvider jwtProvider,
        CancellationToken cancellationToken) =>
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user is null)
        {
            return Results.BadRequest(Result<string>.Failure("Kullanýcý adý ya da þifre yanlýþ"));
        }

        bool checkpassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkpassword)
        {
            return Results.BadRequest(Result<string>.Failure("Kullanýcý adý ya da þifre yanlýþ"));
        }

        var token = jwtProvider.CreateToken();

        return Results.Ok(new { Message = token });
    })
    .Produces<Result<string>>();

app.MigrateDatabase();
app.CreateFirstUser();

app.Run();