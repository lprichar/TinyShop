using DataEntities;
using Products.Endpoints;

namespace Store.Services;

public interface IProductService
{
    Task<List<Product>> GetProducts();
}

public class ProductService(IProductApiService productApiService) : IProductService
{
    public Task<List<Product>> GetProducts()
    {
        return productApiService.GetProducts();
    }
    
}