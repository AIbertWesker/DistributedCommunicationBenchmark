using DCBenchmark.Application.Interfaces;
using DCBenchmark.Application.Requests;
using DCBenchmark.Domain;

namespace DistributedCommunicationBenchmark.Web.GraphQL;

public class Mutations
{
    public Product CreateProduct([Service] IProductRepository productRepository, CreateProductRequest request)
    {
        return productRepository.AddProduct(request);
    }
}
