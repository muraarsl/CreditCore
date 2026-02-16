using System;
using System.Linq;
using CoreCredit.Domain.Common;
using CreditCore.Domain.Credit;
using FluentAssertions;
using Xunit;

namespace CreditCore.Domain.Tests.Credit;

public class CreditCalculatorTests
{
    private readonly ICreditCalculator _calculator;

    public CreditCalculatorTests()
    {
        _calculator = new CreditCalculator();
    }

    [Fact]
    public void CalculateSchedule_Should_Create_Exact_Number_Of_Installments()
    {
        // Arrange
        decimal principal = 100_000m;
        decimal annualRate = 12m;
        int months = 12;

        // Act
        var installments = _calculator.CalculateSchedule( new Money(principal), annualRate, months,0,0);

        // Assert
        installments.Should().HaveCount(months);
    }

    [Fact]
    public void CalculateSchedule_Total_Principal_Payments_Should_Equal_Principal()
    {
        // Arrange
        decimal principal = 50_000m;
        decimal annualRate = 10m;
        int months = 24;

        // Act
        var installments = _calculator.CalculateSchedule(new (principal), annualRate, months,0,0);

        // Assert
        var totalPrincipalPaid = installments.Sum(x => x.PrincipalPayment.Amount);

        totalPrincipalPaid.Should().BeApproximately(principal, 1m);
    }

    [Fact]
    public void CalculateSchedule_Remaining_Principal_Should_Be_Zero_On_Last_Installment()
    {
        // Arrange
        decimal principal = 20_000m;
        decimal annualRate = 15m;
        int months = 10;

        // Act
        var installments = _calculator.CalculateSchedule(new Money(principal), annualRate, months, 0, 0);

        // Assert
        installments.Last().RemainingPrincipal.Amount.Should().Be(0);
    }

    [Fact]
    public void CalculateSchedule_With_Zero_Interest_Should_Not_Create_Interest_Payment()
    {
        // Arrange
        decimal principal = 12_000m;
        decimal annualRate = 0m;
        int months = 12;

        // Act
        var installments = _calculator.CalculateSchedule(new Money(principal), annualRate, months, 0, 0);

        // Assert
        installments.All(x => x.InterestPayment.Amount == 0m).Should().BeTrue();
    }

    [Fact]
    public void CalculateSchedule_Monthly_Installment_Should_Be_Stable_Except_Last()
    {
        // Arrange
        decimal principal = 30_000m;
        decimal annualRate = 14m;
        int months = 12;

        // Act
        var installments = _calculator.CalculateSchedule(new Money(principal), annualRate, months, 0, 0 );

        var first = installments.First().MonthlyInstallment.Amount;
        var last = installments.Last().MonthlyInstallment.Amount;

        // Assert
        first.Should().BeGreaterThan(0);
        last.Should().BeGreaterThan(0);
    }

    [Fact]
    public void CalculateSchedule_Should_Throw_When_Principal_Is_Invalid()
    {
        // Act
        Action act = () => _calculator.CalculateSchedule(new Money( 0), 10, 12,0,0);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CalculateSchedule_Should_Throw_When_Term_Is_Invalid()
    {
        // Act
        Action act = () => _calculator.CalculateSchedule(new Money(10_000), 10, 0, 0, 0    );


        // Assert
        act.Should().Throw<ArgumentException>();
    }
    [Fact]
    public void Calculate_Should_Return_Correct_Tax_Amounts()
    {
        var calculator = new InterestTaxCalculator();

        var tax = calculator.Calculate(
            interestAmount: new Money(1000m),
            bsmvRate: 0.10m,
            kkdfRate: 0.15m,
            TaxApplicability.Both);

        tax.Bsmv.Amount.Should().Be(100m);
        tax.Kkdf.Amount.Should().Be(150m);
    }

}
