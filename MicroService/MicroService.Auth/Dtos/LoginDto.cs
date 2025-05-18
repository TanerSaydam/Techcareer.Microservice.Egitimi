namespace MicroService.Auth.Dtos;

public sealed record LoginDto(
    string UserName,
    string Password);
