using Core.Models;

namespace Core.Data;

public interface IDataStore
{
    IReadOnlyList<Server> Servers { get; }
    IReadOnlyList<Function> Functions { get; }
    IReadOnlyList<Tip> Tips { get; }
}