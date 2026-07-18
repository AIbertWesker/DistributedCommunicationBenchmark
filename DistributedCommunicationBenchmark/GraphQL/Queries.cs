using DCBenchmark.Application.Interfaces;
using DCBenchmark.Domain;

namespace DistributedCommunicationBenchmark.Web.GraphQL;

public class Queries
{
    public Product? GetProductById([Service] IProductRepository productRepository, int id)
    {
        return productRepository.GetProductById(id);
    }

    public IReadOnlyList<Product> GetProducts([Service] IProductRepository productRepository, int pageNumber, int pageSize)
    {
        return productRepository.GetProductsPaginatedList(pageNumber, pageSize);
    }

    //byte[] nie działa, graphQL nie jest stworzony do takiego scenariusza i powinien jedynie zwracac link do endpointu pobioru pliku\
    //obejscie to base64 ktory jest 33% wiekszy rozmiarowo ale do małych plików ujdzie
    public string GetFile([Service] IGenerator fileGenerator, int sizeMb)
    {
        return Convert.ToBase64String(fileGenerator.GetFile(sizeMb));
    }
}
