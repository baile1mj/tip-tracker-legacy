using Core.Models;

namespace Core.Data;

public class DataBuilder
{
    private PayPeriod _payPeriod = null!;
    private readonly List<Server> _servers = new();
    private readonly List<Function> _functions = new();
    private readonly List<TipType> _tipTypes = new();
    private readonly List<Tip> _tips = new();

    private DataBuilder() { }

    public DataBuilder WithServers(IEnumerable<Server> servers)
    {
        if (servers != null)
        {
            _servers.AddRange(servers);
        }

        return this;
    }

    public DataBuilder WithFunctions(IEnumerable<Function> functions)
    {
        if (functions != null)
        {
            _functions.AddRange(functions);
        }

        return this;
    }

    public DataBuilder AddExistingTips(IEnumerable<Tip> tips)
    {
        if (tips != null)
        {
            _tips.AddRange(tips);
        }

        return this;
    }

    public DataBuilder WithTipTypes(IEnumerable<TipType> tipTypes)
    {
        if (tipTypes != null)
        {
            _tipTypes.AddRange(tipTypes);
        }

        return this;
    }

    public DataBuilder ForPayPeriod(PayPeriod payPeriod)
    {
        _payPeriod = payPeriod;
        return this;
    }

    public IDataStore BuildData()
    {
        // The data set must minimally have a pay period defined.
        if (_payPeriod == null) { throw new NullReferenceException("Cannot create service for undefined pay period."); }

        // All parent data elements must be present in the tips.
        var serverLookup = _servers.Select(x => x.ServerId.UniqueId).ToHashSet();
        var functionLookup = _functions.Select(x => x.Id).ToHashSet();

        foreach (var tip in _tips)
        {
            if (!serverLookup.Contains(tip.Recipient.ServerId.UniqueId))
            {
                throw new NullReferenceException("One or more tips is assigned to an undefined server.");
            }

            if (tip.IsFunctionTip && !functionLookup.Contains(tip.Function.Id))
            {
                throw new NullReferenceException("One or more tips is assigned to an undefined function.");
            }

            if (!_tipTypes.Contains(tip.Type))
            {
                throw new NullReferenceException("One or more tips is of an unrecognized type.");
            }
        }

        return new DataStore(_payPeriod);
    }

    public static DataBuilder Create()
    {
        return new DataBuilder();
    }
}