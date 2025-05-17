namespace MicroService.CartWebAPI.Models;

public sealed class Cart
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
}
