namespace DCBenchmark.Application.Interfaces;

public interface IGenerator
{
    byte[] GetFile(int sizeMb);
}
