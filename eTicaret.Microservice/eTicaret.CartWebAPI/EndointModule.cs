using eTicaret.CartWebAPI.Context;
using eTicaret.CartWebAPI.Dtos;
using eTicaret.CartWebAPI.Models;
using TS.Result;

namespace eTicaret.CartWebAPI;

public static class EndointModule
{
    public static IEndpointRouteBuilder MapCartEndpoint(this IEndpointRouteBuilder app, IConfiguration configuration)
    {
        app.MapPost(string.Empty,
            async (
                CreateCartDto request,
                ApplicationDbContext dbContext,
                HttpClient httpClient,
                // ISendEndpointProvider sendEndpointProvider,
                CancellationToken cancellationToken) =>
            {
                #region Product Stok Adedini Güncelle
                //var poductEndpoint = configuration.GetSection("Endpoints:Product").Value;
                //string endpoint = "";
                //var requestObj = new { Id = request.ProductId, Quantity = request.Quantity };

                //var json = JsonSerializer.Serialize(requestObj);
                //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var message = await httpClient.PutAsJsonAsync("http://product-api", request);

                if (!message.IsSuccessStatusCode)
                {
                    var response = await message.Content.ReadFromJsonAsync<Result<string>>();
                    return Results.BadRequest(response);
                }
                #endregion

                #region Sepete Ekle
                Cart cart = new()
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                };

                dbContext.Add(cart);
                try
                {
                    //throw new ArgumentException("hata!");
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(Result<string>.Failure(ex.Message));
                    throw;
                }
                #endregion

                #region Kuyruğa ekle
                //var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-cart-queue"));
                //await sendEndpoint.Send(request);
                #endregion

                return Results.Ok(Result<string>.Succeed("Ürün sepete başarıyla eklendi"));
            })
            .Produces<Result<string>>(); //13:10 görüşelim

        return app;
    }
}
