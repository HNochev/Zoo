using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Infrastructure.Data.Migrations
{
    public partial class EditSomeOfTheNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalEatingType_AnimalEatingTypeId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalKind_AnimalKindId",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalKind",
                table: "AnimalKind");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalEatingType",
                table: "AnimalEatingType");

            migrationBuilder.RenameTable(
                name: "AnimalKind",
                newName: "AnimalKinds");

            migrationBuilder.RenameTable(
                name: "AnimalEatingType",
                newName: "AnimalEatingTypes");

            migrationBuilder.RenameColumn(
                name: "ConditionDescription",
                table: "AnimalEatingTypes",
                newName: "EatingTypeDescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalKinds",
                table: "AnimalKinds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalEatingTypes",
                table: "AnimalEatingTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalEatingTypes_AnimalEatingTypeId",
                table: "Animals",
                column: "AnimalEatingTypeId",
                principalTable: "AnimalEatingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalKinds_AnimalKindId",
                table: "Animals",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalEatingTypes_AnimalEatingTypeId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalKinds_AnimalKindId",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalKinds",
                table: "AnimalKinds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalEatingTypes",
                table: "AnimalEatingTypes");

            migrationBuilder.RenameTable(
                name: "AnimalKinds",
                newName: "AnimalKind");

            migrationBuilder.RenameTable(
                name: "AnimalEatingTypes",
                newName: "AnimalEatingType");

            migrationBuilder.RenameColumn(
                name: "EatingTypeDescription",
                table: "AnimalEatingType",
                newName: "ConditionDescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalKind",
                table: "AnimalKind",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalEatingType",
                table: "AnimalEatingType",
                column: "Id");

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
    }
}
