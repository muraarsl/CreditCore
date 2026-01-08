using LendFlow.Domain.Entities;
using LendFlow.Domain.Validators;
using FluentValidation.TestHelper; // Fluent test yardımcıları için şart


namespace LendFlow.Domain.Tests
{
    public class LoanAccountValidatorTests
    {
        private readonly LoanAccountValidator _validator;

        public LoanAccountValidatorTests()
        {
            _validator = new LoanAccountValidator();
        }

        [Fact]
        public void Should_Have_Error_When_AccountReference_Is_Empty()
        {
            // Arrange
            var account = new LoanAccount { AccountReference = "" };

            // Act & Assert (Fluent Style)
            var result = _validator.TestValidate(account);

            result.ShouldHaveValidationErrorFor(x => x.AccountReference)
                  .WithErrorMessage("Account reference is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Balance_Is_Negative()
        {
            // Arrange
            var account = new LoanAccount { AvailableBalance = -100m };

            // Act & Assert
            var result = _validator.TestValidate(account);

            result.ShouldHaveValidationErrorFor(x => x.AvailableBalance);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Data_Is_Valid()
        {
            // Arrange
            var account = new LoanAccount
            {
                AccountReference = "ACC-99999",
                AvailableBalance = 1000m,
                InternalCreditRating = 500
            };

            // Act & Assert
            var result = _validator.TestValidate(account);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
