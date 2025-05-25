namespace eTicaret.CartWebAPI.Dtos;

public sealed record CreateCartDto(
    Guid ProductId,
    int Quantity);
