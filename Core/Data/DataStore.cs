using Core.Models;
using Core.Validation;

namespace Core.Data;

internal class DataStore : IDataStore
{
    private readonly PayPeriod _payPeriod;
    private readonly List<Server> _servers = new();
    private readonly List<Function> _functions = new();
    private readonly List<Tip> _tips = new();

    public IReadOnlyList<Server> Servers => _servers.AsReadOnly();

    public IReadOnlyList<Function> Functions => _functions.AsReadOnly();

    public IReadOnlyList<Tip> Tips => _tips.AsReadOnly();

    public DataStore(PayPeriod payPeriod)
    {
        _payPeriod = payPeriod;
    }

    public DataStore(List<Server> servers, List<Function> functions, List<Tip> tips, PayPeriod payPeriod)
    {
        _payPeriod = payPeriod;
        _servers.AddRange(servers);
        _functions.AddRange(functions);
        _tips.AddRange(tips);
    }
}