using CreditCore.Domain.Credit;

namespace CreditCore.API.Contracts;

public class CreateCreditRequest
{
    public decimal Principal { get; set; }
    public int TermInMonths { get; set; }
    public decimal AnnualInterestRate { get; set; }

    public TaxApplicability TaxApplicability { get; set; }
}