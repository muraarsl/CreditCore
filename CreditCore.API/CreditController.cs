using CreditCore.API.Contracts;
using CreditCore.Application.Contracts;
using CreditCore.Application.Services;
using CreditCore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CreditCore.Contracts;

namespace CreditCore.API.Controllers;

[ApiController]
[Route("credits")]
public class CreditController : ControllerBase
{
    
    private readonly CreditCalculationService _calculationService;
    private readonly CreditDbContext _dbContext;

    public CreditController(CreditCalculationService calculationService, CreditDbContext dbContext)
    {
        
        _calculationService = calculationService;
        _dbContext = dbContext;
    }
    
    
    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] CreateCreditRequest request)
    {
        var calculation = _calculationService.Calculate(
            request.Principal,
            request.TermInMonths,
            request.AnnualInterestRate,
            true);

        var creditEntity = new CreditEntity
        {
            Id = Guid.NewGuid(),
            PrincipalAmount = request.Principal,
            TermInMonths = request.TermInMonths,
            AnnualInterestRate = request.AnnualInterestRate,
            CreatedAt = DateTime.UtcNow,
            Installments = calculation.Installments.Select(i => new InstallmentEntity
            {
                Id = Guid.NewGuid(),
                InstallmentNo = i.InstallmentNo,
                PrincipalPayment = i.PrincipalPayment,
                InterestPayment = i.InterestPayment,
                Bsmv = i.Bsmv,
                Kkdf = i.Kkdf,
                RemainingBalance = i.RemainingBalance
            }).ToList()
        };

        var occurredOnUtc = DateTime.UtcNow;

        await _dbContext.Credits.AddAsync(creditEntity);
        await _dbContext.OutboxMessages.AddAsync(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = "credit.calculated.v1",
            Payload = JsonSerializer.Serialize(new CreditCalculatedEvent(
                creditEntity.Id,
                request.Principal,
                request.TermInMonths,
                request.AnnualInterestRate,
                calculation.TotalPayment,
                occurredOnUtc)),
            OccurredOnUtc = occurredOnUtc
        });
        await _dbContext.SaveChangesAsync();

        return Ok(calculation);
    }



}
