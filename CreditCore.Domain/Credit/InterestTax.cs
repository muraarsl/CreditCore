using CoreCredit.Domain.Common;

namespace CreditCore.Domain.Credit;

/// <summary>
/// Faiz üzerinden hesaplanan vergi tutarlarını temsil eder.
/// </summary>
public sealed record InterestTax(
    Money Bsmv,
    Money Kkdf
);
