namespace MicroService.ProductWebAPI.Models;

public sealed class Product
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = default!;
    public int Stock { get; set; }
}