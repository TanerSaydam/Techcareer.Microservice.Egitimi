﻿namespace eTicaret.ProductWebAPI.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
}