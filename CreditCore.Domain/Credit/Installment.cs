using CoreCredit.Domain.Common;

namespace CreditCore.Domain.Credit
{
    public class Installment
    {
        public int InstallmentNo { get; }
        public Money PrincipalPayment { get; }
        public Money InterestPayment { get; }
        public Money MonthlyInstallment { get; }
        public Money RemainingPrincipal { get; }
        public decimal Rate { get; }

        public Installment(
            int installmentNo,
            Money principalPayment,
            Money interestPayment,
            Money monthlyInstallment,
            Money remainingPrincipal,
            decimal rate)
        {
            InstallmentNo = installmentNo;
            PrincipalPayment = principalPayment;
            InterestPayment = interestPayment;
            MonthlyInstallment = monthlyInstallment;
            RemainingPrincipal = remainingPrincipal;
            Rate = rate;
        }
    }
}
