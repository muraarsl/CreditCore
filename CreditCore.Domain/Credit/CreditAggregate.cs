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
        Principal = principal; 
        AnnualInterestRate = annualInterestRate;
        TermInMonths = termInMonths;
        Installments = installments;
    }
}