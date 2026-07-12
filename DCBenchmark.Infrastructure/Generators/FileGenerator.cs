using DCBenchmark.Application.Interfaces;

namespace DCBenchmark.Infrastructure.Generators;

internal sealed class FileGenerator : IGenerator
{
    private readonly Dictionary<int, byte[]> _generatedFiles = [];

    public void Generate(int sizeMb)
    {
        var data = new byte[sizeMb * 1024 * 1024];
        Random.Shared.NextBytes(data);
        _generatedFiles.TryAdd(sizeMb, data);
    }

    public byte[] GetFile(int sizeMb)
    {
        if (!_generatedFiles.TryGetValue(sizeMb, out var data))
        {
            throw new ArgumentOutOfRangeException(nameof(sizeMb));
        }
        return data;
    }
}
