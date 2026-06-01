using CoreCredit.Domain.Common;
using System;
using System.Collections.Generic;

namespace CreditCore.Domain.Credit;

public class CreditCalculator : ICreditCalculator
{
    public IReadOnlyCollection<Installment> Calculate(
        decimal principal,
        int termInMonths,decimal kkdfRate,
        decimal bsmvRate)
    {
        const decimal defaultAnnualRate = 0;
        return CalculateSchedule(new Money(principal), defaultAnnualRate, termInMonths, kkdfRate,
         bsmvRate);
    }

    public IReadOnlyList<Installment> CalculateSchedule(Money principal, decimal annualRate, int months, decimal kkdfRate, decimal bsmvRate)
    
    {
        if (principal.Amount <= 0)
            throw new ArgumentException("Principal must be greater than zero");

        if (annualRate < 0)
            throw new ArgumentException("Annual rate cannot be negative");

        if (months <= 0)
            throw new ArgumentException("Term must be greater than zero");

        var installments = new List<Installment>();

        decimal monthlyRate = annualRate / 12m / 100m;

        decimal monthlyInstallment =
            monthlyRate == 0
                ? principal.Amount / months
                : principal.Amount * monthlyRate /
                  (1 - (decimal)Math.Pow((double)(1 + monthlyRate), -months));

        monthlyInstallment = decimal.Round(monthlyInstallment, 2);

        decimal remainingPrincipal = principal.Amount;

        for (int i = 1; i <= months; i++)
        {
            decimal interestPayment =
                decimal.Round(remainingPrincipal * monthlyRate, 2);

            decimal principalPayment =
                decimal.Round(monthlyInstallment - interestPayment, 2);

            decimal kkdfPayment =
                decimal.Round(interestPayment * kkdfRate / 100m, 2);

            decimal bsmvPayment =
                decimal.Round(interestPayment * bsmvRate / 100m, 2);

            if (i == months)
            {
                principalPayment = remainingPrincipal;

                monthlyInstallment =
                    principalPayment +
                    interestPayment;
            }

            decimal totalDue =
                monthlyInstallment +
                kkdfPayment +
                bsmvPayment;

            decimal endingBalance =
                decimal.Round(remainingPrincipal - principalPayment, 2);

            installments.Add(new Installment(
                installmentNo: i,
                principalPayment: new Money(principalPayment),
                interestPayment: new Money(interestPayment),
                //kkdfPayment: new Money(kkdfPayment),
                //bsmvPayment: new Money(bsmvPayment),
                monthlyInstallment: new Money(monthlyInstallment), // anapara + faiz
                //totalDueAmount: new Money(totalDue),              // müşterinin ödeyeceği gerçek tutar
                remainingPrincipal: new Money(endingBalance),
                rate: annualRate
            ));

            remainingPrincipal = endingBalance;
        }

        return installments;
    }
}
