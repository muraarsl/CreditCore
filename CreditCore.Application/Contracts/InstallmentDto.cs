using CoreCredit.Domain.Common;

namespace CreditCore.Application.Contracts.CreditCalculation;


public record InstallmentDto(
    int InstallmentNo,
    decimal PrincipalPayment,
    decimal InterestPayment,
    decimal Bsmv,
    decimal Kkdf,
    decimal RemainingBalance
);
