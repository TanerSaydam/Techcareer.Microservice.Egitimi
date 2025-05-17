var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MicroService_CartWebAPI>("cart-webapi");
builder.AddProject<Projects.MicroService_Gateway>("gateway");
builder.AddProject<Projects.MicroService_ProductWebAPI>("product-weapi");

builder.Build().Run(); //metric  // open telemety
