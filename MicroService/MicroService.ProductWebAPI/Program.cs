using MicroService.ProductWebAPI.Enpoints;

var builder = WebApplication.CreateBuilder(args);

//Service Registration // Service Container // Dependecy Injection Par�as�
builder.AddServiceDefaults();
var app = builder.Build();

//Middleware

app.MapProducts();

//extensions method
app.Run();
