using FluentValidation;
using LendFlow.Domain.Entities;

namespace LendFlow.Domain.Validators
{
    public class LoanAccountValidator : AbstractValidator<LoanAccount>
    {
        public LoanAccountValidator()
        {
            RuleFor(x => x.AccountReference)
                .NotEmpty().WithMessage("Account reference is required.")
                .Length(5, 50).WithMessage("Reference must be between 5 and 50 characters.");

            RuleFor(x => x.AvailableBalance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative.");

            RuleFor(x => x.InternalCreditRating)
                .InclusiveBetween(0, 1000).WithMessage("Credit rating must be between 0 and 1000.");
        }
    }
}