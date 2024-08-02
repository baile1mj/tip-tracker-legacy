using Core.Validation;

namespace Core.Models;

public class Function
{
    public Guid Id { get; }
    public string Name { get; }
    public DateOnly Date { get; }

    public Function(string name, DateOnly date)
        : this(Guid.Empty, name, date)
    { }

    public Function(Guid id, string name, DateOnly date)
    {
        GuardAgainst.NullOrEmptyString(name);

        Id = id == Guid.Empty
            ? Guid.NewGuid()
            : id;
        Name = name;
        Date = date;
    }

    public override string ToString()
    {
        return Name;
    }
}