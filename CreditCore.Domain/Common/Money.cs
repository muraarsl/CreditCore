namespace CoreCredit.Domain.Common;

/// <summary>
/// Domain'de "para" kavramını temsil eder.
/// Decimal değildir, davranışı vardır.
/// </summary>
public readonly record struct Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Money cannot be negative");

        Amount = decimal.Round(amount, 2);
    }

    public static Money Zero => new(0);

    public Money Add(Money other)
        => new(Amount + other.Amount);

    public Money Subtract(Money other)
    {
        if (other.Amount > Amount)
            throw new InvalidOperationException("Insufficient amount");

        return new Money(Amount - other.Amount);
    }
}
