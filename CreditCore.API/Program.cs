using CreditCore.Application.Services;
using CreditCore.Domain.Credit;
using CreditCore.Infrastructure;
using CreditCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddSingleton<IConnectionMultiplexer>(
//    ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



builder.Services.AddScoped<ICreditCalculator, CreditCalculator>();
builder.Services.AddScoped<ITaxRateProvider, RedisTaxRateProvider>();
//builder.Services.AddSingleton<IConnectionMultiplexer>(
//    ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect("redis:6379"));

builder.Services.AddScoped<InterestTaxCalculator>();
builder.Services.AddScoped<CreditCalculationService>();

var app = builder.Build();
app.MapGet("/health", () => Results.Ok(new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow
}));


app.UseSwagger();
    app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
 
}
app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();
app.Run();
