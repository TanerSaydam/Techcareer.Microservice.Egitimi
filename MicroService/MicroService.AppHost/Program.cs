var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MicroService_CartWebAPI>("cart-webapi");
builder.AddProject<Projects.MicroService_Gateway>("gateway");
builder.AddProject<Projects.MicroService_ProductWebAPI>("product-webapi");
builder.AddProject<Projects.MicroService_Auth>("auth");

builder.Build().Run(); //metric  // open telemety