using System.Diagnostics;
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
    public static ActivitySource Source = new("TinyShop.DistributedTracing", "1.0.0");

    public async Task<List<Product>> GetProducts()
    {
        using var activity = Source.StartActivity();
        await Task.Delay(1234);
        return await db.Product.ToListAsync();
    }
}