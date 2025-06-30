namespace TipTracker.Ui.DataObjects;

/// <summary>
/// Contains data about a server.
/// </summary>
 public class Server
{
    /// <summary>
    /// The GUID representing the server.
    /// </summary>
    public Guid UniqueId { get; }

    /// <summary>
    /// The value used in POS system for this server, which may not
    /// be unique.
    /// </summary>
    public string PosId { get; }

    /// <summary>
    /// Gets or sets the server's last name.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Gets the server's first name.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Creates a new instance of a server.
    /// </summary>
    /// <param name="posId">The server's ID in the POS system.</param>
    /// <param name="lastName">The server's last name.</param>
    /// <param name="firstName">The server's first name.</param>
    public Server(string posId, string lastName, string firstName)
        : this(Guid.NewGuid(), posId, lastName, firstName)
    { }

    /// <summary>
    /// Creates a new instance of a server.
    /// </summary>
    /// <param name="uniqueId">A unique ID for the server.</param>
    /// <param name="posId">The server's ID in the POS system.</param>
    /// <param name="lastName">The server's last name.</param>
    /// <param name="firstName">The server's first name.</param>
    public Server(Guid uniqueId, string posId, string lastName, string firstName)
    {
        if (uniqueId == Guid.Empty)
        {
            throw new ArgumentException("You must specify a valid GUID for the unique ID.");
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(posId);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);

        UniqueId = uniqueId;
        PosId = posId;
        LastName = lastName;
        FirstName = firstName;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{PosId}: {LastName}, {FirstName}";
    }

    /// <summary>
    /// Gets the server's full name.
    /// </summary>
    /// <returns>A string containing the server's full name.</returns>
    public string FullName()
    {
        return $"{LastName}, {FirstName}";
    }

    /// <summary>
    /// Gets the string representations for this server to use in a lookup list.
    /// </summary>
    /// <returns>The collection of string representations for looking up this server.</returns>
    public IEnumerable<string> GetLookupValues()
    {
        yield return ToString();
        yield return $"{FirstName} {LastName}";
        yield return $"{LastName}, {FirstName}";
    }
}