using DCBenchmark.Application.Interfaces;
using DistributedCommunicationBenchmark.Grpc;
using Grpc.Core;

namespace DistributedCommunicationBenchmark.Web.Grpc;

public class ProductGrpcService(IProductRepository productRepository, IGenerator fileGenerator) : ProductService.ProductServiceBase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IGenerator _fileGenerator = fileGenerator;

    public override Task<ProductReply> GetProductById(ProductRequest request, ServerCallContext context)
    {
        var product = _productRepository.GetProductById(request.ProductId)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {request.ProductId} not found."));

        var reply = new ProductReply
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = (double)product.Price,
            Material = product.Material,
            Ean = product.EAN
        };
        return Task.FromResult(reply);
    }

    public override Task<ProductListReply> GetProducts(ProductListRequest request, ServerCallContext context)
    {
        var products = _productRepository.GetProductsPaginatedList(request.PageNumber, request.PageSize);
        var reply = new ProductListReply();
        foreach (var product in products)
        {
            reply.Products.Add(new ProductReply
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Material = product.Material,
                Ean = product.EAN
            });
        }
        return Task.FromResult(reply);
    }

    public override Task<ProductReply> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        var product = new DCBenchmark.Application.Requests.CreateProductRequest
        {
            Name = request.Name,
            Description = request.Description,
            Price = (decimal)request.Price,
            Material = request.Material,
        };
        var result = _productRepository.AddProduct(product);
        var reply = new ProductReply
        {
            ProductId = result.Id,
            Name = result.Name,
            Description = result.Description,
            Price = (double)result.Price,
            Material = result.Material,
            Ean = result.EAN
        };
        return Task.FromResult(reply);
    }

    public override Task<FileReply> DownloadFile(FileRequest request, ServerCallContext context)
    {
        var result = _fileGenerator.GetFile(request.SizeMb);
        var reply = new FileReply
        {
            Data = Google.Protobuf.ByteString.CopyFrom(result)
        };
        return Task.FromResult(reply);
    }
}
