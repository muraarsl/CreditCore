using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCore.Domain.Credit
{
    public class MonthlyInterestCalculator : IMontlyInterestCalculator
    {
        public decimal Calculate(decimal amount, decimal interestRate)
        {
            return Math.Round(interestRate / 12m / 100m, 6);
        }
    }
}
