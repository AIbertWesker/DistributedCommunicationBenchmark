using DCBenchmark.Application.Interfaces;
using DCBenchmark.Infrastructure.Generators;
using DCBenchmark.Infrastructure.InMemoryRepos;
using DCBenchmark.Infrastructure.Seeder;
using Microsoft.Extensions.DependencyInjection;

namespace DCBenchmark.Infrastructure;

public static class DIRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, int seederDataCount)
    {
        var products = ProductSeeder.Generate(seederDataCount);
        services.AddSingleton<IProductRepository>(new InMemoryProductRepository(products));

        var fileGenerator = new FileGenerator();
        fileGenerator.Generate(1);
        fileGenerator.Generate(10);
        fileGenerator.Generate(50);
        services.AddSingleton<IGenerator>(fileGenerator);

        return services;
    }
}
