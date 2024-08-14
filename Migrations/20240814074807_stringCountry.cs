using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValeShop.Migrations
{
    /// <inheritdoc />
    public partial class stringCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingDetails_States_StateId",
                table: "BillingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillingDetails_StateId",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "BillingDetails");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "BillingDetails",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "BillingDetails",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BillingDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "BillingDetails",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BillingDetails_StateId",
                table: "BillingDetails",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingDetails_States_StateId",
                table: "BillingDetails",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
