namespace CreditCore.Application.Interfaces;

public interface ICreditCalculationService
{
    CreditCalculationResponse Calculate(decimal principal, int termInMonths, decimal AnnualInterestRate,
       bool taxAvaliablity);
}
