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
    public async Task<List<Product>> GetProducts()
    {
        await Task.Delay(1234);
        return await db.Product.ToListAsync();
    }
}