using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PRINCIPAL_AMOUNT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TERM_IN_MONTHS = table.Column<int>(type: "INTEGER", nullable: false),
                    ANNUAL_INTEREST_RATE = table.Column<decimal>(type: "TEXT", precision: 9, scale: 6, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ID", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    INSTALLMENT_NO = table.Column<int>(type: "INTEGER", nullable: false),
                    PRINCIPAL_PAYMENT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    INTEREST_PAYMENT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    BSMV_AMOUNT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    KKDF_AMOUNT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    REMAINING_BALANCE = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CREDIT_ID = table.Column<Guid>(type: "TEXT", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_Credits_CREDIT_ID",
                        column: x => x.CREDIT_ID,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installments_CREDIT_ID_INSTALLMENT_NO",
                table: "Installments",
                columns: new[] { "CREDIT_ID", "INSTALLMENT_NO" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Credits");
        }
    }
}
