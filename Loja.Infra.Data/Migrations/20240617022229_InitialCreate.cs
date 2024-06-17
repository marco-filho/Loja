using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Loja");

            migrationBuilder.CreateTable(
                name: "Pedido",
                schema: "Loja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false, comment: "Nome do cliente"),
                    OrderedAt = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "Data e hora em que o pedido foi feito"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                },
                comment: "Registro de pedidos");

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                schema: "Loja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Product = table.Column<string>(type: "TEXT", nullable: false, comment: "Nome do produto"),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false, comment: "Quantidade do produto no pedido"),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false, comment: "Preço unitário do produto"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Loja",
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Itens de pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_OrderId",
                schema: "Loja",
                table: "ItemPedido",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedido",
                schema: "Loja");

            migrationBuilder.DropTable(
                name: "Pedido",
                schema: "Loja");
        }
    }
}
