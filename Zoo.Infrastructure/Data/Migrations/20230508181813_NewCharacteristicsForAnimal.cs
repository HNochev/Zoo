using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Infrastructure.Data.Migrations
{
    public partial class NewCharacteristicsForAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalKind",
                table: "Animals",
                newName: "AnimalName");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalEatingTypeId",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalKindId",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AnimalEatingType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalEatingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimalFeedings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HourOfEating = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalFeedings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalFeedings_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalKind",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimalsKind = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalKind", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalEatingTypeId",
                table: "Animals",
                column: "AnimalEatingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalKindId",
                table: "Animals",
                column: "AnimalKindId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalFeedings_AnimalId",
                table: "AnimalFeedings",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalEatingType_AnimalEatingTypeId",
                table: "Animals",
                column: "AnimalEatingTypeId",
                principalTable: "AnimalEatingType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalKind_AnimalKindId",
                table: "Animals",
                column: "AnimalKindId",
                principalTable: "AnimalKind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalEatingType_AnimalEatingTypeId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalKind_AnimalKindId",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "AnimalEatingType");

            migrationBuilder.DropTable(
                name: "AnimalFeedings");

            migrationBuilder.DropTable(
                name: "AnimalKind");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AnimalEatingTypeId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AnimalKindId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AnimalEatingTypeId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AnimalKindId",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "AnimalName",
                table: "Animals",
                newName: "AnimalKind");
        }
    }
}
