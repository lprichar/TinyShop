using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var products = builder.AddProject<Products>("products");

builder.AddProject<Store>("store")
    .WithExternalHttpEndpoints()
    .WithReference(products);

builder.Build().Run();
