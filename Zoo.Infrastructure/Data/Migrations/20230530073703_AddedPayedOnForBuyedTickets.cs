using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Infrastructure.Data.Migrations
{
    public partial class AddedPayedOnForBuyedTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayedOn",
                table: "Tickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayedOn",
                table: "BuyedTickets",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayedOn",
                table: "BuyedTickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayedOn",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }
    }
}
