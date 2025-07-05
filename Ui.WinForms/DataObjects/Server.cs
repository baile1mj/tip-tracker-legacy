using TipTracker.Ui.ViewData;

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

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="Server.PosId"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByNumber(Server x, Server y)
    {
        var numberRelation = string.Compare(x.PosId, y.PosId, StringComparison.Ordinal);
        if (numberRelation != 0) { return numberRelation; }

        // Numbers are the same, so now let's compare last names.
        var lastNameRelation = string.CompareOrdinal(x.LastName, y.LastName);
        if (lastNameRelation != 0) { return lastNameRelation; }

        // Both the server number and last name are the same, so now let's check by first name.
        var firstNameRelation = string.CompareOrdinal(x.FirstName, y.FirstName);
        return firstNameRelation;
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="Server.LastName"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByLastName(Server x, Server y)
    {
        var lastNameRelation = string.CompareOrdinal(x.LastName, y.LastName);
        if (lastNameRelation != 0) { return lastNameRelation; }

        // Last names are the same, so now let's compare first names.
        var firstNameRelation = string.CompareOrdinal(x.FirstName, y.FirstName);
        if (firstNameRelation != 0) { return firstNameRelation; }

        // Servers have the same name, so try to distinguish by server number.
        var numberRelation = string.Compare(x.PosId, y.PosId, StringComparison.Ordinal);
        return numberRelation;
    }

    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other, based
    /// on the <see cref="Server.FirstName"/> property.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// <list type="table">
    ///     <listheader><term> Value</term><description> Meaning</description></listheader>
    ///     <item>
    ///         <term> Less than zero</term>
    ///         <description><paramref name="x" /> is less than <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Zero</term>
    ///         <description><paramref name="x" /> equals <paramref name="y" />.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description><paramref name="x" /> is greater than <paramref name="y" />.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public static int CompareByFirstName(Server x, Server y)
    {
        var firstNameRelation = string.CompareOrdinal(x.FirstName, y.FirstName);
        if (firstNameRelation != 0) { return firstNameRelation; }

        // First names are the same, so now let's compare last names.
        var lastNameRelation = string.CompareOrdinal(x.LastName, y.LastName);
        if (lastNameRelation != 0) { return lastNameRelation; }

        // Servers have the same name, so try to distinguish by server number.
        var numberRelation = string.Compare(x.PosId, y.PosId, StringComparison.Ordinal);
        return numberRelation;
    }
}