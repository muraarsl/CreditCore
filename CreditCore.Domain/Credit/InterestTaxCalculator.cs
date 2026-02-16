using CoreCredit.Domain.Common;

namespace CreditCore.Domain.Credit;

public sealed class InterestTaxCalculator
{
    public InterestTax Calculate(
        Money interestAmount,
        decimal bsmvRate,
        decimal kkdfRate,
        TaxApplicability applicability)
    {
        if (interestAmount.Amount < 0)
            throw new ArgumentException("Interest amount cannot be negative");

        var bsmv = applicability is TaxApplicability.OnlyBsmv or TaxApplicability.Both
            ? decimal.Round(interestAmount.Amount * bsmvRate, 2)
            : 0m;

        var kkdf = applicability is TaxApplicability.OnlyKkdf or TaxApplicability.Both
            ? decimal.Round(interestAmount.Amount * kkdfRate, 2)
            : 0m;

        return new InterestTax(new Money( bsmv), new Money(kkdf));
    }
}
