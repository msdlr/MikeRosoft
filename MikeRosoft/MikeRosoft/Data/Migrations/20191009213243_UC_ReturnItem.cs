using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeRosoft.Data.Migrations
{
    public partial class UC_ReturnItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnRequests_AspNetUsers_userId",
                table: "ReturnRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReturnRequests_userId",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ReturnRequests");

            migrationBuilder.CreateTable(
                name: "UserRequests",
                columns: table => new
                {
                    requestID = table.Column<int>(nullable: false),
                    userID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => new { x.userID, x.requestID });
                    table.ForeignKey(
                        name: "FK_UserRequests_ReturnRequests_requestID",
                        column: x => x.requestID,
                        principalTable: "ReturnRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRequests_AspNetUsers_userID",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_requestID",
                table: "UserRequests",
                column: "requestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRequests");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "ReturnRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequests_userId",
                table: "ReturnRequests",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnRequests_AspNetUsers_userId",
                table: "ReturnRequests",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
