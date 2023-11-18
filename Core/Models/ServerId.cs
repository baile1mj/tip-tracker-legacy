using Core.Validation;

namespace Core.Models;

public class ServerId : IComparable<ServerId>, IEquatable<ServerId>
{
    public string PosId { get; }

    public Guid UniqueId { get; }

    public ServerId(string posId)
        : this(posId, Guid.Empty)
    { }

    public ServerId(string posId, Guid uniqueId)
    {
        GuardAgainst.NullOrEmptyString(posId);

        PosId = posId;
        UniqueId = uniqueId == Guid.Empty
            ? Guid.NewGuid()
            : uniqueId;
    }

    public override string ToString()
    {
        return PosId;
    }

    /// <inheritdoc />
    public int CompareTo(ServerId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;

        return UniqueId.CompareTo(other.UniqueId);
    }

    /// <inheritdoc />
    public bool Equals(ServerId other)
    {
        return UniqueId.Equals(other.UniqueId);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ServerId)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return UniqueId.GetHashCode();
    }

    public static bool operator ==(ServerId? left, ServerId? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ServerId? left, ServerId? right)
    {
        return !Equals(left, right);
    }
}