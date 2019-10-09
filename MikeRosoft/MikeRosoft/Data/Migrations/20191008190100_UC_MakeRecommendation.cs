using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeRosoft.Data.Migrations
{
    public partial class UC_MakeRecommendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_productId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    IdRecommendation = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 180, nullable: false),
                    adminId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.IdRecommendation);
                    table.ForeignKey(
                        name: "FK_Recommendations_AspNetUsers_adminId",
                        column: x => x.adminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecommendations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productid = table.Column<int>(nullable: true),
                    recommendationIdRecommendation = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecommendations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Recommendations_recommendationIdRecommendation",
                        column: x => x.recommendationIdRecommendation,
                        principalTable: "Recommendations",
                        principalColumn: "IdRecommendation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRecommendations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recommendationIdRecommendation = table.Column<int>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecommendations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRecommendations_Recommendations_recommendationIdRecommendation",
                        column: x => x.recommendationIdRecommendation,
                        principalTable: "Recommendations",
                        principalColumn: "IdRecommendation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRecommendations_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_productid",
                table: "ProductRecommendations",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_recommendationIdRecommendation",
                table: "ProductRecommendations",
                column: "recommendationIdRecommendation");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_adminId",
                table: "Recommendations",
                column: "adminId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecommendations_recommendationIdRecommendation",
                table: "UserRecommendations",
                column: "recommendationIdRecommendation");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecommendations_userId",
                table: "UserRecommendations",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Products_productId",
                table: "ProductOrder",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Products_productId",
                table: "ProductOrder");

            migrationBuilder.DropTable(
                name: "ProductRecommendations");

            migrationBuilder.DropTable(
                name: "UserRecommendations");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_productId",
                table: "ProductOrder",
                column: "productId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
