using eTicaret.ProductWebAPI.Context;
using eTicaret.ProductWebAPI.Dtos;
using eTicaret.ProductWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eTicaret.ProductWebAPI;

public static class EndpoitModule
{
    public static IEndpointRouteBuilder MapProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("{id}",
            async (Guid id, ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
                if (product is null)
                {
                    return Results.BadRequest(Result<Product>.Failure("Ürün bulunamadı"));
                }
                return Results.Ok(Result<Product>.Succeed(product));
            })
            .Produces<Result<Product>>();

        app.MapGet(string.Empty,
            async (ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
            {
                var products = await dbContext.Products.OrderBy(p => p.Name).ToListAsync(cancellationToken);
                return products;
            })
            .Produces<List<Product>>();

        app.MapPut(string.Empty,
            async (ProductStockUpdateDto request, ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(i => i.Id == request.ProductId, cancellationToken);

                //throw new ArgumentException("Test test");

                if (product is null)
                {
                    return Results.BadRequest(Result<string>.Failure("Ürün bulunamadı"));
                }

                product.Stock -= request.Quantity;
                dbContext.Update(product);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Results.Ok(Result<string>.Succeed("Ürün stok bilgisi başarıyla güncellendi"));
            })
            .Produces<Result<string>>();

        return app;
    }
}
