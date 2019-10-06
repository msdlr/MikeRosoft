using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeRosoft.Data.Migrations
{
    public partial class ClassesForBanUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BanForUserList",
                table: "BanForUserList");

            migrationBuilder.DropIndex(
                name: "IX_BanForUserList_GetBanID",
                table: "BanForUserList");

            migrationBuilder.AlterColumn<string>(
                name: "GetUserId",
                table: "BanForUserList",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "BanForUserList",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BanForUserList",
                table: "BanForUserList",
                columns: new[] { "GetBanID", "GetUserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BanForUserList",
                table: "BanForUserList");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "BanForUserList",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "GetUserId",
                table: "BanForUserList",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BanForUserList",
                table: "BanForUserList",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_BanForUserList_GetBanID",
                table: "BanForUserList",
                column: "GetBanID");
        }
    }
}
