namespace Bookify.Domain.Apartments;

public sealed record Address(
    string State,
    string ZipCode,
    string City,
    string Street);