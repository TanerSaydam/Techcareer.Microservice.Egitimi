using eTicaret.ProductWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicaret.ProductWebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(builder =>
        {
            builder.Property(p => p.Name).HasColumnType("varchar(100)");
            builder.Property(p => p.Price).HasColumnType("money");
        });
    }
}
