using System;
using System.Collections.Generic;

namespace CreditCore.Infrastructure.Persistence
{
    public class CreditEntity
    {
        public Guid Id { get; set; }

        public decimal PrincipalAmount { get; set; }
        public int TermInMonths { get; set; }
        public decimal AnnualInterestRate { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<InstallmentEntity> Installments { get; set; }
            = new List<InstallmentEntity>();
    }
}
