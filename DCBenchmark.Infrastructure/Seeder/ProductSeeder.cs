using Bogus;
using DCBenchmark.Domain;

namespace DCBenchmark.Infrastructure.Seeder;

internal static class ProductSeeder
{
    public static List<Product> Generate(int count)
    {
        var fake = new Faker<Product>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Material, f => f.Commerce.ProductMaterial())
            .RuleFor(p => p.EAN, f => f.Commerce.Ean13())
            .RuleFor(x => x.Price, f => f.Random.Decimal(10, 1000));

        return fake.Generate(count);
    }
}
