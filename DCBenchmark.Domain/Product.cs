namespace DCBenchmark.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
