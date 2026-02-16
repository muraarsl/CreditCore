using System.Linq;
using CreditCore.Domain.Credit;
using CreditCore.Infrastructure.Persistence;

namespace CreditCore.Infrastructure.Persistence.Mapping
{
    public static class CreditMapping
    {
        public static CreditEntity ToEntity(this CreditAggregate domain)
        {
            return new CreditEntity
            {
                Id = domain.Id,
                PrincipalAmount = domain.Principal,
                TermInMonths = domain.TermInMonths,
                Installments = domain.Installments.Select(i => new InstallmentEntity
                {
                    InstallmentNo = i.InstallmentNo,
                    PrincipalPayment = i.PrincipalPayment.Amount,
                    InterestPayment = i.InterestPayment.Amount,
                    RemainingBalance = i.RemainingPrincipal.Amount
                }).ToList()
            };
        }
    }
}
