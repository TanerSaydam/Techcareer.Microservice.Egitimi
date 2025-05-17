using MicroService.ProductWebAPI.Models;

namespace MicroService.ProductWebAPI.Enpoints;

public static class ProductModule
{
    public static IEndpointRouteBuilder MapProducts(this IEndpointRouteBuilder app)
    {

        var group = app.MapGroup(string.Empty);

        group.MapGet("{id}", (Guid id) => //controller dan daha performanslı
        {
            Product product = new()
            {
                Name = "Domates",
                Stock = 5
            };

            return Results.Ok(product);
        });

        group.MapGet(string.Empty, () => //controller dan daha performanslı
        {
            List<Product> products = new();
            Product product = new()
            {
                Name = "Domates"
            };
            products.Add(product);

            return Results.Ok(products);
        });



        return app;
    }
}
