namespace TipTracker;

public class ServerComparer : IEqualityComparer<Server>
{
    public bool Equals(Server? x, Server? y)
    {
        if (ReferenceEquals(x, y)) { return true; }
        if (x is null) { return false; }
        if (y is null) { return false; }
        if (x.GetType() != y.GetType()) { return false; }

        return x.Id == y.Id && x.FirstName.Equals(y.FirstName, StringComparison.OrdinalIgnoreCase) 
            && x.LastName.Equals(y.LastName, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(Server obj)
    {
        return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName);
    }
}