using DCBenchmark.Application.Requests;
using DCBenchmark.Domain;

namespace DCBenchmark.Application.Interfaces;

public interface IProductRepository
{
    public Product AddProduct(CreateProductRequest request);
    public Product? GetProductById(int id);
    public IReadOnlyList<Product> GetProductsPaginatedList(int pageNumber, int pageSize);
}
