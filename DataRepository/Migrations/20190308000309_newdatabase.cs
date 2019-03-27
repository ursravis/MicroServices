using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataRepository.Migrations
{
    public partial class newdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(maxLength: 10, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.UniqueConstraint("AK_Customers_EmailId", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    VehicleTypeId = table.Column<int>(nullable: false),
                    VehicleTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.VehicleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PricePerMonth = table.Column<double>(nullable: false),
                    MaxCoverage = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()"),
                    LastModified = table.Column<DateTime>(nullable: false, computedColumnSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Customer_Quotes",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleNumber = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "VehicleTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleQuote",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleQuote", x => new { x.VehicleId, x.QuoteId });
                    table.ForeignKey(
                        name: "FK_VehicleQuote_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleQuote_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "VehicleTypeId", "VehicleTypeName" },
                values: new object[,]
                {
                    { 1, "SUV" },
                    { 2, "Sedan" },
                    { 3, "Truck" },
                    { 4, "EV" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CustomerId_StartDate",
                table: "Quotes",
                columns: new[] { "CustomerId", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleQuote_QuoteId",
                table: "VehicleQuote",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleQuote");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
