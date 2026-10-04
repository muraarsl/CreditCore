using CreditCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCore.Application.Services
{
    public class CreditCalculationOperation
    {
        public static async Task SaveCalculationToDatabase(CreditDbContext dbContext, CreditCalculationResponse calculationResponse)
        {
            CreditEntity creditEntity = new CreditEntity();
            creditEntity.PrincipalAmount = calculationResponse.Principal;
            creditEntity.TermInMonths = calculationResponse.Installments.Count;
            creditEntity.AnnualInterestRate = calculationResponse.AnnualInterestRate;
            creditEntity.CreatedAt = DateTime.UtcNow;
            creditEntity.Installments = new List<InstallmentEntity>();

            foreach (var i in calculationResponse.Installments)
                creditEntity.Installments.Add(new InstallmentEntity
                {
                    InstallmentNo = i.InstallmentNo,
                    PrincipalPayment = i.PrincipalPayment,
                    InterestPayment = i.InterestPayment,
                    Bsmv = i.Bsmv,
                    Kkdf = i.Kkdf,
                    RemainingBalance = i.RemainingBalance
                });

            await dbContext.Credits.AddAsync(creditEntity);
            await dbContext.SaveChangesAsync();

        }


       
            



        }
    }

