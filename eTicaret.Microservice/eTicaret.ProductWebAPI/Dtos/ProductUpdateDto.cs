namespace eTicaret.ProductWebAPI.Dtos;

public sealed record ProductUpdateDto(
    Guid Id,
    int Quantity);


public sealed record ProductStockUpdateDto(
    Guid ProductId,
    int Quantity);