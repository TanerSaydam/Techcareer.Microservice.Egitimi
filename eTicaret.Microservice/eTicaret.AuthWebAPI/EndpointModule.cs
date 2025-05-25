using eTicaret.AuthWebAPI.Dtos;
using eTicaret.AuthWebAPI.Models.Users;
using eTicaret.AuthWebAPI.Services;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eTicaret.AuthWebAPI;

public static class EndpointModule
{
    public static IEndpointRouteBuilder MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
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
                return Results.BadRequest(Result<string>.Failure("Kullanıcı adı ya da şifre yanlış"));
            }

            bool checkpassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!checkpassword)
            {
                return Results.BadRequest(Result<string>.Failure("Kullanıcı adı ya da şifre yanlış"));
            }

            var token = jwtProvider.CreateToken();

            return Results.Ok(new { Message = token });
        })
        .Produces<Result<string>>();

        return app;
    }
}
