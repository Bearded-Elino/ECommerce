using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValeShop.Migrations
{
    /// <inheritdoc />
    public partial class GuidToState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingDetails_States_StateId1",
                table: "BillingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillingDetails_StateId1",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "StateId1",
                table: "BillingDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "BillingDetails",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingDetails_States_StateId",
                table: "BillingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillingDetails_StateId",
                table: "BillingDetails");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "BillingDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<Guid>(
                name: "StateId1",
                table: "BillingDetails",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BillingDetails_StateId1",
                table: "BillingDetails",
                column: "StateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingDetails_States_StateId1",
                table: "BillingDetails",
                column: "StateId1",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
