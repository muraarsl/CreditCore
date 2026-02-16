using CoreCredit.Domain.Common;
using System.Collections.Generic;

namespace CreditCore.Domain.Credit;

public interface ICreditCalculator
{
    //IReadOnlyCollection<Installment> Calculate(
    //     Money principal,
    //    int termInMonths);

    IReadOnlyList<Installment> CalculateSchedule(
        Money principal,
        decimal annualRate,
        int months,
        decimal kkdfRate,
        decimal bsmvRate);
}
