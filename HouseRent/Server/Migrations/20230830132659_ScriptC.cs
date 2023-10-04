using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent.Server.Migrations
{
    public partial class ScriptC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flats",
                columns: table => new
                {
                    FlatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flats", x => x.FlatID);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantID);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentDate = table.Column<DateTime>(type: "date", nullable: false),
                    BookedDate = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TenantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentID);
                    table.ForeignKey(
                        name: "FK_Rents_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentItems",
                columns: table => new
                {
                    RentID = table.Column<int>(type: "int", nullable: false),
                    FlatID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentItems", x => new { x.RentID, x.FlatID });
                    table.ForeignKey(
                        name: "FK_RentItems_Flats_FlatID",
                        column: x => x.FlatID,
                        principalTable: "Flats",
                        principalColumn: "FlatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentItems_Rents_RentID",
                        column: x => x.RentID,
                        principalTable: "Rents",
                        principalColumn: "RentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantID", "Address", "Email", "TenantName" },
                values: new object[] { 1, "Mirpur", "Manjurul12@gmail.com", "Manjurul" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantID", "Address", "Email", "TenantName" },
                values: new object[] { 2, "Mirpur", "Manjurul12@gmail.com", "syed" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantID", "Address", "Email", "TenantName" },
                values: new object[] { 3, "Mirpur", "Manjurul12@gmail.com", "syed" });

            migrationBuilder.CreateIndex(
                name: "IX_RentItems_FlatID",
                table: "RentItems",
                column: "FlatID");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_TenantID",
                table: "Rents",
                column: "TenantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentItems");

            migrationBuilder.DropTable(
                name: "Flats");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
