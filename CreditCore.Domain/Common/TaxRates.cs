namespace CoreCredit.Domain.Credit;

/// <summary>
/// Faiz üzerinden alınan vergi oranlarını temsil eder.
/// </summary>
public sealed record TaxRates
{
    public decimal Bsmv { get; }
    public decimal Kkdf { get; }

    public TaxRates(decimal bsmv, decimal kkdf)
    {
        if (bsmv < 0 || kkdf < 0)
            throw new ArgumentException("Tax rates cannot be negative");

        if (bsmv > 1 || kkdf > 1)
            throw new ArgumentException("Tax rates must be decimal ratios (e.g. 0.15)");

        Bsmv = bsmv;
        Kkdf = kkdf;
    }

    public static TaxRates None => new(0, 0);
}
