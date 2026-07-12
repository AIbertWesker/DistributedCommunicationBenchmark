using DCBenchmark.Application.Interfaces;
using DCBenchmark.Application.Requests;
using DCBenchmark.Domain;

namespace DCBenchmark.Infrastructure.InMemoryRepos;

internal sealed class InMemoryProductRepository(List<Product> products) : IProductRepository
{
    private readonly List<Product> _products = products;

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public IReadOnlyList<Product> GetProductsPaginatedList(int pageNumber, int pageSize)
    {
        return [.. _products.Skip((pageNumber - 1) * pageSize).Take(pageSize)];
    }

    public Product AddProduct(CreateProductRequest request)
    {
        var result = new Product
        {
            Id = _products.Count + 1,
            Name = request.Name,
            Description = request.Description,
            Material = request.Material,
            Price = request.Price,
            EAN = Guid.NewGuid().ToString()
        };
        //Note: W prawdziwym przypadku tu dokonano by dodania do bazy danych.
        //Z racji tego ze serwis jest singletonem i to jest tylko test, ten step jest pomijany.
        return result;
    }
}
