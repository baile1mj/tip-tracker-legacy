using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

public class Server
{
    public Guid UniqueId { get; }
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public bool PrintChit { get; }
    public bool IsActive { get; }

    public Server(Guid uniqueId, string id, string firstName, string lastName, bool printChit)
        : this(uniqueId, id, firstName, lastName, printChit, true)
    { }

    public Server(Guid uniqueId, string id, string firstName, string lastName, bool printChit, bool isActive)
    {
        UniqueId = uniqueId;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PrintChit = printChit;
        IsActive = isActive;
    }

    public override string ToString()
    {
        return $"{ServerId} {LastName}, {FirstName}";
    }
}