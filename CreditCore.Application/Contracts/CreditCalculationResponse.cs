using CreditCore.Application.Contracts.CreditCalculation;

public record CreditCalculationResponse(
    decimal Principal,
    decimal AnnualInterestRate,
    decimal MonthlyInterestRate,
    decimal MonthlyInstallment,
    decimal TotalPayment,
    decimal TotalInterest,
    IReadOnlyList<InstallmentDto> Installments
);