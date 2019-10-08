using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeRosoft.Data.Migrations
{
    public partial class ClassesForBanUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanForUserList_AspNetUsers_UserId",
                table: "BanForUserList");

            migrationBuilder.DropForeignKey(
                name: "FK_BanList_AspNetUsers_AdminId",
                table: "BanList");

            migrationBuilder.DropIndex(
                name: "IX_BanList_AdminId",
                table: "BanList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BanForUserList",
                table: "BanForUserList");

            migrationBuilder.DropIndex(
                name: "IX_BanForUserList_GetBanID",
                table: "BanForUserList");

            migrationBuilder.DropIndex(
                name: "IX_BanForUserList_UserId",
                table: "BanForUserList");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "BanList");

            migrationBuilder.DropColumn(
                name: "GetAdmin",
                table: "BanList");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BanForUserList");

            migrationBuilder.AddColumn<string>(
                name: "GetAdminId",
                table: "BanList",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "GetAdminId",
                table: "BanList");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "BanList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetAdmin",
                table: "BanList",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BanForUserList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BanForUserList",
                table: "BanForUserList",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_BanList_AdminId",
                table: "BanList",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BanForUserList_GetBanID",
                table: "BanForUserList",
                column: "GetBanID");

            migrationBuilder.CreateIndex(
                name: "IX_BanForUserList_UserId",
                table: "BanForUserList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanForUserList_AspNetUsers_UserId",
                table: "BanForUserList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BanList_AspNetUsers_AdminId",
                table: "BanList",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
