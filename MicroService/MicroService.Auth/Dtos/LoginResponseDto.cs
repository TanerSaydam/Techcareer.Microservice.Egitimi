namespace MicroService.Auth.Dtos;

public record LoginResponseDto(
    string Token,
    string RefresToken,
    DateTimeOffset RefreshTokenExpires);
