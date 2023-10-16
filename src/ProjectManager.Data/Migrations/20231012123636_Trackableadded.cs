using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace ProjectManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Trackableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Instant>(
                name: "CreateAt",
                table: "Todo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Todo",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Instant>(
                name: "DeleteAt",
                table: "Todo",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "Todo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "ModifiedAt",
                table: "Todo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Todo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Todo");
        }
    }
}
