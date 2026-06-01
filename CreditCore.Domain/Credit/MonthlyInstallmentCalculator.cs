using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCore.Domain.Credit
{
    public class MonthlyInstallmentCalculator : IMonthlyInstallmentCalculator
    {
        public decimal Calculate(decimal principal, int termInMonths, decimal AnnualInterestRate, decimal kkdf, decimal bsmv)
        {
            double monthlyRate = (double)AnnualInterestRate / 100 / 12.0;
            double pow = Math.Pow(1 + monthlyRate, termInMonths);

            double installment =
                ((double)principal * monthlyRate * pow) /
                (pow - 1);

            double totalTax = (double)(kkdf + bsmv);


            return (decimal)(installment );

            return (decimal)(installment + totalTax);

        }
    }
}
