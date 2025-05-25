using eTicaret.ProductWebAPI.Context;
using eTicaret.ProductWebAPI.Dtos;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace eTicaret.ProductWebAPI.Consumers;

public sealed class UpdateStockConsumer(
    ApplicationDbContext dbContext) : IConsumer<CreateCartDto>
{
    public async Task Consume(ConsumeContext<CreateCartDto> context)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(i => i.Id == context.Message.ProductId);

        if (product is null)
        {
            throw new ArgumentException("Ürün bulunamadı");
        }

        product.Stock -= context.Message.Quantity;
        dbContext.Update(product);
        await dbContext.SaveChangesAsync();
    }
}