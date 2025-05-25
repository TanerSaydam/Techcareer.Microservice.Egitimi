var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy")); ;

builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()
.SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

app.MapReverseProxy();

app.Run();