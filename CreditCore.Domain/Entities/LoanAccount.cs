using System;

namespace LendFlow.Domain.Entities
{
    /// <summary>
    /// Represents the core financial account structure for credit operations.
    /// Manages balance liquidity and debt status monitoring.
    /// </summary>
    public class LoanAccount
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Unique identifier for the bank account (e.g., IBAN or Internal ID).
        /// </summary>
        public string AccountReference { get; set; }

        /// <summary>
        /// Total available liquid funds in the primary currency.
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Flag indicating if all liabilities associated with this account are settled.
        /// </summary>
        public bool IsLiabilitySettled { get; set; } = true;

        /// <summary>
        /// Internal risk assessment score based on credit history.
        /// </summary>
        public int InternalCreditRating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastTransactionDate { get; set; }

        /// <summary>
        /// Executes the liquidation of outstanding liabilities using available balance.
        /// </summary>
        /// <param name="settlementAmount">The total amount to be cleared.</param>
        public void FinalizeSettlement(decimal settlementAmount)
        {
            if (AvailableBalance >= settlementAmount)
            {
                AvailableBalance -= settlementAmount;
                IsLiabilitySettled = true;
                LastTransactionDate = DateTime.Now;
            }
        }
    }
}