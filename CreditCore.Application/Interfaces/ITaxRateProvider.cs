namespace CreditCore.Application.Interfaces;

/// <summary>
/// Vergi oranlarının kaynağını temsil eder (Redis, DB, API vs).
/// </summary>
public interface ITaxRateProvider
{
    decimal GetBsmvRate();
    decimal GetKkdfRate();
}
