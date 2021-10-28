using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Database.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2021, 10, 27, 20, 30, 53, 846, DateTimeKind.Utc).AddTicks(5702)),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Coin = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinValues", x => new { x.Id, x.Time });
                    table.ForeignKey(
                        name: "FK_CoinValues_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastChanged = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Coin = table.Column<string>(type: "text", nullable: false),
                    Quantitiy = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2021, 10, 27, 20, 30, 53, 849, DateTimeKind.Utc).AddTicks(9506)),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioValues", x => new { x.Id, x.Time });
                    table.ForeignKey(
                        name: "FK_PortfolioValues_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinValues_UserId",
                table: "CoinValues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioEntries_Coin_UserId",
                table: "PortfolioEntries",
                columns: new[] { "Coin", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioEntries_UserId",
                table: "PortfolioEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioValues_UserId",
                table: "PortfolioValues",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinValues");

            migrationBuilder.DropTable(
                name: "PortfolioEntries");

            migrationBuilder.DropTable(
                name: "PortfolioValues");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
