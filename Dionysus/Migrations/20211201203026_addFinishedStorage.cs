using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dionysus.Migrations
{
    public partial class addFinishedStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "finished_storage",
                table: "batch",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "finished_storage",
                table: "batch");
        }
    }
}
