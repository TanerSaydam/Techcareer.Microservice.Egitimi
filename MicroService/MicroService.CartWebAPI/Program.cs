using MicroService.CartWebAPI.Dtos;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();


app.MapOpenApi();
app.MapScalarApiReference();

app.MapPost(string.Empty, async (
    CreateCartDto request,
    HttpClient httpClient, CancellationToken cancellationToken
    ) =>
{
    var message = await httpClient.GetAsync($"http://localhost:5102/{request.ProductId}");

    var res = await message.Content.ReadFromJsonAsync<ProductDto>();

    if (res!.Stock < request.Quantity)
    {
        throw new ArgumentException("Stok yeterli değil");
    }

    //db işlemi yap

    return Results.Ok(new { Message = "Ürün sepete başarıyla eklendi" });
});

app.Run();
