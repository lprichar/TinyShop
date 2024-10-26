using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Products>("api");

builder.AddProject<Store>("store");

builder.Build().Run();
