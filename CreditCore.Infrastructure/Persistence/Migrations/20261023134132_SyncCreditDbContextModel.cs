using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncCreditDbContextModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Credits_CreditId",
                table: "Installments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credits",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "InstallmentNo",
                table: "Installments",
                newName: "INSTALLMENT_NO");

            migrationBuilder.RenameColumn(
                name: "CreditId",
                table: "Installments",
                newName: "CREDIT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Installments_CreditId_InstallmentNo",
                table: "Installments",
                newName: "IX_Installments_CREDIT_ID_INSTALLMENT_NO");

            migrationBuilder.RenameColumn(
                name: "TermInMonths",
                table: "Credits",
                newName: "TERM_IN_MONTHS");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Credits",
                newName: "CREATED_AT");

            migrationBuilder.AddPrimaryKey(
                name: "ID",
                table: "Credits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Credits_CREDIT_ID",
                table: "Installments",
                column: "CREDIT_ID",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Credits_CREDIT_ID",
                table: "Installments");

            migrationBuilder.DropPrimaryKey(
                name: "ID",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "INSTALLMENT_NO",
                table: "Installments",
                newName: "InstallmentNo");

            migrationBuilder.RenameColumn(
                name: "CREDIT_ID",
                table: "Installments",
                newName: "CreditId");

            migrationBuilder.RenameIndex(
                name: "IX_Installments_CREDIT_ID_INSTALLMENT_NO",
                table: "Installments",
                newName: "IX_Installments_CreditId_InstallmentNo");

            migrationBuilder.RenameColumn(
                name: "TERM_IN_MONTHS",
                table: "Credits",
                newName: "TermInMonths");

            migrationBuilder.RenameColumn(
                name: "CREATED_AT",
                table: "Credits",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credits",
                table: "Credits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Credits_CreditId",
                table: "Installments",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
