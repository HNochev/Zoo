using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Infrastructure.Data.Migrations
{
    public partial class AddedTransport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportLineTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypeDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportLineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportLineTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortRouteDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullRouteDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinutesDistanceFromFirstStop = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_TransportLineTypes_TransportLineTypeId",
                        column: x => x.TransportLineTypeId,
                        principalTable: "TransportLineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_TransportLineTypeId",
                table: "Transports",
                column: "TransportLineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_UserId",
                table: "Transports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "TransportLineTypes");
        }
    }
}
