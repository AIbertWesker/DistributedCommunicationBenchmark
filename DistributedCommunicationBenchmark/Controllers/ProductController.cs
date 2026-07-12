using DCBenchmark.Application.Interfaces;
using DCBenchmark.Application.Requests;
using DCBenchmark.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DistributedCommunicationBenchmark.Web.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IGenerator _fileGenerator;

    public ProductController(IProductRepository productRepository, 
        IGenerator fileGenerator)
    {
        _productRepository = productRepository;
        _fileGenerator = fileGenerator;
    }

    [HttpGet("single")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = _productRepository.GetProductById(id);
        return Ok(result);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetProductPaginatedList(int pageNumber, int pageSize)
    {
        if(pageNumber < 0 || pageSize <= 0)
            return BadRequest("Page number must be greater than or equal to 0 and page size must be greater than 0.");

        var result = _productRepository.GetProductsPaginatedList(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = _productRepository.AddProduct(request);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpGet("download")]
    public async Task<IActionResult> GetFile(int sizeMb)
    {
        var result = _fileGenerator.GetFile(sizeMb);
        return File(result, "application/octet-stream");
    }
}
