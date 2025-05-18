using MicroService.Auth.Dtos;
using MicroService.Auth.Options;
using MicroService.Auth.Services;
using Scalar.AspNetCore;
using TS.Result;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
// sonra ortam adýna göre: Development/Test/Release
       .AddJsonFile(
           $"appsettings.{builder.Environment.EnvironmentName}.json",
           optional: true,
           reloadOnChange: true
       );

builder.Services.AddOpenApi();

builder.Services.AddScoped<JwtProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

WebApplication app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/", () => "Hello World!");
app.MapGet("/get-connection-string", (IConfiguration configuration) =>
{
    string connectionString = configuration.GetConnectionString("SqlServer") ?? "Empty";
    return Results.Ok(Result<string>.Succeed(connectionString));
});

app.MapPost("/login", async (
    LoginDto request,
    JwtProvider jwtProvider,
    CancellationToken cancellationToken) =>
{
    await Task.CompletedTask;
    //Db griþ kontrolü

    if (request.UserName == "admin" && request.Password == "1")
    {
        LoginResponseDto token = jwtProvider.CreateToken();
        return Results.Ok(Result<LoginResponseDto>.Succeed(token));
    }

    string errorMessage = "Kullanýcý adý ya þifre bilgin yanlýþ";
    return Results.BadRequest(Result<string>.Failure(400, errorMessage));
}).Produces<Result<LoginResponseDto>>();

app.Run();


//dotnet run --launch-profile Test

//12:45 görüþelim