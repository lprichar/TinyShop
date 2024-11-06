using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    //.WithRedisInsights();
    .WithRedisCommander()
    .WithPersistence(interval: TimeSpan.FromMinutes(5));

var products = builder.AddProject<Products>("products")
    .WithReference(cache);

builder.AddProject<Store>("store")
    .WithReference(products);

builder.Build().Run();
