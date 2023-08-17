namespace Bookify.Domain.Apartments;

public sealed record Currency
{
    public string Code { get; init; }

    public static readonly Currency None = new("");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");

    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Usd,
        Eur
    };

    private Currency(string code) => Code = code;

    public static Currency FromCode(string code) => All.FirstOrDefault(c => c.Code == code) ??
                                                    throw new ApplicationException("Invalid currency code");
}