using System;

namespace CreditCore.Infrastructure.Persistence
{
    public class InstallmentEntity
    {
        public Guid Id { get; set; }

        public int InstallmentNo { get; set; }

        public decimal PrincipalPayment { get; set; }
        public decimal InterestPayment { get; set; }
        public decimal Bsmv { get; set; }
        public decimal Kkdf { get; set; }

        public decimal RemainingBalance { get; set; }

        // FK
        public Guid CreditId { get; set; }

        // Navigation
        public CreditEntity Credit { get; set; }
    }
}
