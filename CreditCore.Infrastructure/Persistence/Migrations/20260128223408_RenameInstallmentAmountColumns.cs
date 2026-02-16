using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameInstallmentAmountColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingBalance",
                table: "Installments",
                newName: "REMAINING_BALANCE");

            migrationBuilder.RenameColumn(
                name: "PrincipalPayment",
                table: "Installments",
                newName: "PRINCIPAL_PAYMENT");

            migrationBuilder.RenameColumn(
                name: "Kkdf",
                table: "Installments",
                newName: "KKDF_AMOUNT");

            migrationBuilder.RenameColumn(
                name: "InterestPayment",
                table: "Installments",
                newName: "INTEREST_PAYMENT");

            migrationBuilder.RenameColumn(
                name: "Bsmv",
                table: "Installments",
                newName: "BSMV_AMOUNT");

            migrationBuilder.RenameColumn(
                name: "PrincipalAmount",
                table: "Credits",
                newName: "PRINCIPAL_AMOUNT");

            migrationBuilder.RenameColumn(
                name: "AnnualInterestRate",
                table: "Credits",
                newName: "ANNUAL_INTEREST_RATE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "REMAINING_BALANCE",
                table: "Installments",
                newName: "RemainingBalance");

            migrationBuilder.RenameColumn(
                name: "PRINCIPAL_PAYMENT",
                table: "Installments",
                newName: "PrincipalPayment");

            migrationBuilder.RenameColumn(
                name: "KKDF_AMOUNT",
                table: "Installments",
                newName: "Kkdf");

            migrationBuilder.RenameColumn(
                name: "INTEREST_PAYMENT",
                table: "Installments",
                newName: "InterestPayment");

            migrationBuilder.RenameColumn(
                name: "BSMV_AMOUNT",
                table: "Installments",
                newName: "Bsmv");

            migrationBuilder.RenameColumn(
                name: "PRINCIPAL_AMOUNT",
                table: "Credits",
                newName: "PrincipalAmount");

            migrationBuilder.RenameColumn(
                name: "ANNUAL_INTEREST_RATE",
                table: "Credits",
                newName: "AnnualInterestRate");
        }
    }
}
