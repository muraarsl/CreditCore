using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCore.Domain.Credit
{
    internal interface IMonthlyInstallmentCalculator
    {
        decimal Calculate(decimal principal, int termInMonths, decimal AnnualInterestRate, decimal kkdf, decimal bsmv);
    }
}
