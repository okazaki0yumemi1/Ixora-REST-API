using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ixora_REST_API.Migrations
{
    /// <inheritdoc />
    public partial class meow3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_GoodsTypes_GoodsTypeID",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Goods_GoodsId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClientId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_GoodsId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Goods_GoodsTypeID",
                table: "Goods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_GoodsId",
                table: "OrderDetails",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GoodsTypeID",
                table: "Goods",
                column: "GoodsTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_GoodsTypes_GoodsTypeID",
                table: "Goods",
                column: "GoodsTypeID",
                principalTable: "GoodsTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Goods_GoodsId",
                table: "OrderDetails",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
