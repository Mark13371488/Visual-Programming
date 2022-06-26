using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsApp1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                });

            migrationBuilder.CreateTable(
                name: "Performancers",
                columns: table => new
                {
                    PerformancerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScienceDegree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProBiography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performancers", x => x.PerformancerId);
                });

            migrationBuilder.CreateTable(
                name: "Conferentions",
                columns: table => new
                {
                    ConferentionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferentions", x => x.ConferentionId);
                    table.ForeignKey(
                        name: "FK_Conferentions_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    NameOfHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    ConferentionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_Sections_Conferentions_ConferentionId",
                        column: x => x.ConferentionId,
                        principalTable: "Conferentions",
                        principalColumn: "ConferentionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformancerId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    StartOfPerformance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.PerformanceId);
                    table.ForeignKey(
                        name: "FK_Performances_Performancers_PerformancerId",
                        column: x => x.PerformancerId,
                        principalTable: "Performancers",
                        principalColumn: "PerformancerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performances_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPerformance",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPerformance", x => new { x.EquipmentId, x.PerformanceId });
                    table.ForeignKey(
                        name: "FK_EquipmentPerformance_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentPerformance_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "PerformanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conferentions_BuildingId",
                table: "Conferentions",
                column: "BuildingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPerformance_PerformanceId",
                table: "EquipmentPerformance",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_PerformancerId",
                table: "Performances",
                column: "PerformancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_SectionId",
                table: "Performances",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ConferentionId",
                table: "Sections",
                column: "ConferentionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_RoomId",
                table: "Sections",
                column: "RoomId",
                unique: true,
                filter: "[RoomId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentPerformance");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Performancers");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Conferentions");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
