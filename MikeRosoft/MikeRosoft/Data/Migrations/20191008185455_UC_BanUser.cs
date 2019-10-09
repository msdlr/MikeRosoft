using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeRosoft.Data.Migrations
{
    public partial class UC_BanUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "BanTypeList");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DefaultDuration",
                table: "BanTypeList",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultDuration",
                table: "BanTypeList");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "BanTypeList",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
