using CoreCredit.Domain.Common;
using CreditCore.Application.Contracts;
using CreditCore.Application.Contracts.CreditCalculation;
using CreditCore.Application.Interfaces;
using CreditCore.Domain.Credit;
using StackExchange.Redis;
using System.Linq;

namespace CreditCore.Application.Services;

public class CreditCalculationService : ICreditCalculationService
{
    private readonly ICreditCalculator _creditCalculator;
    private readonly InterestTaxCalculator _taxCalculator;
    private readonly ITaxRateProvider _taxRateProvider;
    public CreditCalculationService(ICreditCalculator creditCalculator, ITaxRateProvider taxRateProvider, InterestTaxCalculator taxCalculator)
    {
        _creditCalculator = creditCalculator;
        _taxCalculator = taxCalculator;
        _taxRateProvider = taxRateProvider;
    }

    public CreditCalculationResponse Calculate(
     decimal principal,
     int termInMonths,
     decimal annualInterestRate,
     bool taxAvailability)
    {
        var bsmvRate = taxAvailability
            ? _taxRateProvider.GetBsmvRate()
            : 0m;

        var kkdfRate = taxAvailability
            ? _taxRateProvider.GetKkdfRate()
            : 0m;
    
        var installments = _creditCalculator
            .CalculateSchedule(new Money(principal), annualInterestRate, termInMonths, kkdfRate,bsmvRate);



        var installmentDtos = installments.Select(i =>
        {
            var tax = _taxCalculator.Calculate(
                i.InterestPayment,
                bsmvRate,
                kkdfRate,
                TaxApplicability.Both);

            return new InstallmentDto(
                InstallmentNo: i.InstallmentNo,
                PrincipalPayment: i.PrincipalPayment.Amount,
                InterestPayment: i.InterestPayment.Amount,
                Bsmv: tax.Bsmv.Amount,
                Kkdf: tax.Kkdf.Amount,
                RemainingBalance: i.RemainingPrincipal.Amount
            );
        }).ToList();

         MonthlyInterestCalculator monthlyInterestCalculator = new MonthlyInterestCalculator();
         decimal monthlyInterestRate = monthlyInterestCalculator.Calculate(principal, annualInterestRate);

        MonthlyInstallmentCalculator monthlyInstallmentCalculator = new MonthlyInstallmentCalculator();
        decimal monthlyInstallment = monthlyInstallmentCalculator.Calculate(installmentDtos.First().PrincipalPayment,termInMonths, annualInterestRate,
              installmentDtos.First().Kkdf
               , installmentDtos.First().Bsmv);

        return new CreditCalculationResponse(
            Principal: principal,
            AnnualInterestRate: annualInterestRate,
            MonthlyInterestRate: monthlyInterestRate,
            MonthlyInstallment:monthlyInstallment,
                
            TotalPayment: Math.Round(
                installmentDtos.Sum(x =>
                    x.PrincipalPayment
                    + x.InterestPayment
                    + x.Bsmv
                    + x.Kkdf), 2),
            TotalInterest: Math.Round(
                installmentDtos.Sum(x => x.InterestPayment), 2),
            Installments: installmentDtos
        );
    }

}
