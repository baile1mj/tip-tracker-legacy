using Core.Models;
using Core.Validation;

namespace Core.Data;

public class DataStore : IDataStore
{
    private readonly List<Server> _servers = new();
    private readonly List<Function> _functions = new();
    private readonly List<Tip> _tips = new();
    private readonly List<TipType> _tipTypes = new();

    public PayPeriod PayPeriod { get; }

    public IReadOnlyList<Server> Servers => _servers.AsReadOnly();

    public IReadOnlyList<Function> Functions => _functions.AsReadOnly();

    public IReadOnlyList<Tip> Tips => _tips.AsReadOnly();

    public IReadOnlyList<TipType> TipTypes => _tipTypes.AsReadOnly();

    public DataStore(PayPeriod payPeriod)
    {
        PayPeriod = payPeriod;
    }

    public DataStore(PayPeriod payPeriod, List<Server> servers, List<Function> functions, List<TipType> tipTypes,
        List<Tip> tips)
    {
        PayPeriod = payPeriod;
        _servers.AddRange(servers);
        _functions.AddRange(functions);
        _tipTypes.AddRange(tipTypes);
        _tips.AddRange(tips);
    }
}