namespace eTicaret.CartWebAPI.Models;

public sealed class Cart
{
    public Cart()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}