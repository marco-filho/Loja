using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Pedido_OrderId",
                schema: "Loja",
                table: "ItemPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                schema: "Loja",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPedido",
                schema: "Loja",
                table: "ItemPedido");

            migrationBuilder.RenameTable(
                name: "Pedido",
                schema: "Loja",
                newName: "Order",
                newSchema: "Loja");

            migrationBuilder.RenameTable(
                name: "ItemPedido",
                schema: "Loja",
                newName: "OrderItem",
                newSchema: "Loja");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPedido_OrderId",
                schema: "Loja",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                schema: "Loja",
                table: "Order",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                comment: "Valor somado dos itens no pedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "Loja",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "Loja",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Loja",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "Loja",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "Loja",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "Loja",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "Loja",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "Loja",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "Loja",
                newName: "ItemPedido",
                newSchema: "Loja");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "Loja",
                newName: "Pedido",
                newSchema: "Loja");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Loja",
                table: "ItemPedido",
                newName: "IX_ItemPedido_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPedido",
                schema: "Loja",
                table: "ItemPedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                schema: "Loja",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Pedido_OrderId",
                schema: "Loja",
                table: "ItemPedido",
                column: "OrderId",
                principalSchema: "Loja",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
