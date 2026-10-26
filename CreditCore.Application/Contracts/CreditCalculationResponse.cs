using CreditCore.Application.Contracts.CreditCalculation;

namespace CreditCore.Application.Contracts
{ 
    public record CreditCalculationResponse(
        decimal Principal,
        decimal AnnualInterestRate,
        decimal MonthlyInterestRate,
        decimal MonthlyInstallment,
        decimal TotalPayment,
        decimal TotalInterest,
        IReadOnlyList<InstallmentDto> Installments
    );
}