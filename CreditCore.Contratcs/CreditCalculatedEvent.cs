namespace CreditCore.Contracts { 
     public sealed record CreditCalculatedEvent(
        Guid CreditId,
        decimal Principal,
        int TermInMonths,
        decimal AnnualInterestRate,
        decimal TotalPayment,
        DateTime OccurredOnUtc);
}