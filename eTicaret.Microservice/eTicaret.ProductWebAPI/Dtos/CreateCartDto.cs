namespace eTicaret.ProductWebAPI.Dtos;

public sealed record CreateCartDto(
    Guid ProductId,
    int Quantity);
