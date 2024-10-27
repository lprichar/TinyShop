using System.Diagnostics;
using System.Diagnostics.Metrics;
using DataEntities;
using Microsoft.EntityFrameworkCore;
using Products.Data;

namespace Products.Endpoints;

public interface IProductApiService
{
    Task<List<Product>> GetProducts();
}

public class ProductApiService(ProductDataContext db) : IProductApiService
{
    private static ActivitySource Source = new("TinyShop.DistributedTracing", "1.0.0");
    private static Meter productsMeter = new ("TinyShop.Products", "1.0.0");

    public async Task<List<Product>> GetProducts()
    {
        using var activity = Source.StartActivity();
        activity?.SetTag("username", "fred");
        var countGreetings = productsMeter.CreateCounter<int>("Products.Requests", description: "Counts the number of greetings");
        var histogram = productsMeter.CreateHistogram<int>("Products.Duration", "Num", "Number of products");
        
        productsMeter.CreateObservableCounter<int>("Products.ObservableRequests", 
            () => new Random().Next(10));

        countGreetings.Add(1);
        histogram.Record(new Random().Next(100));

        await Task.Delay(1234);
        countGreetings.Add(-1);
        return await db.Product.ToListAsync();
    }
}