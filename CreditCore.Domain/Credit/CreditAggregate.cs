namespace CreditCore.Domain.Credit;

public class CreditAggregate
{
    public Guid Id { get; } = Guid.NewGuid();

    public decimal Principal { get; }
    public decimal AnnualInterestRate { get; }
    public int TermInMonths { get; }

    public IReadOnlyList<Installment> Installments { get; }

    public CreditAggregate(
        decimal principal,
        decimal annualInterestRate,
        int termInMonths,
        IReadOnlyList<Installment> installments)
    {
        if (principal <= 0)
            throw new ArgumentException("Principal must be greater than zero", nameof(principal));

        if (annualInterestRate < 0)
            throw new ArgumentException("Annual interest rate cannot be negative", nameof(annualInterestRate));

        if (termInMonths <= 0)
            throw new ArgumentException("Term must be greater than zero", nameof(termInMonths));

        if (installments is null)
            throw new ArgumentNullException(nameof(installments));

        if (installments.Count != termInMonths)
            throw new ArgumentException("Installment count must match the term", nameof(installments));

        if (installments[^1].RemainingPrincipal != CoreCredit.Domain.Common.Money.Zero)
            throw new ArgumentException("The final installment must clear the principal balance", nameof(installments));

        Principal = principal;
        AnnualInterestRate = annualInterestRate;
        TermInMonths = termInMonths;
        Installments = Array.AsReadOnly(installments.ToArray());
    }

    public static CreditAggregate Create(
        decimal principal,
        decimal annualInterestRate,
        int termInMonths,
        decimal kkdfRate,
        decimal bsmvRate,
        ICreditCalculator calculator)
    {
        ArgumentNullException.ThrowIfNull(calculator);

        var installments = calculator.CalculateSchedule(
            new CoreCredit.Domain.Common.Money(principal),
            annualInterestRate,
            termInMonths,
            kkdfRate,
            bsmvRate);

        return new CreditAggregate(principal, annualInterestRate, termInMonths, installments);
    }
}
