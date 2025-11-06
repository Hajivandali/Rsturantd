using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInvoiceItems_CustomerInvoices_InvoiceReference",
                table: "CustomerInvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuReference",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Products_ProductReference",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Products_ProductReference",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ProductReference",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuReference",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_ProductReference",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInvoiceItems_InvoiceReference",
                table: "CustomerInvoiceItems");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Customers");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Prices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MenuId",
                table: "MenuItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "MenuItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "CustomerInvoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "CustomerInvoices",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "CustomerInvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "CustomerInvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "CustomerInvoiceItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductId",
                table: "Prices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ProductId",
                table: "MenuItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoiceItems_InvoiceId",
                table: "CustomerInvoiceItems",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInvoiceItems_CustomerInvoices_InvoiceId",
                table: "CustomerInvoiceItems",
                column: "InvoiceId",
                principalTable: "CustomerInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Products_ProductId",
                table: "MenuItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Products_ProductId",
                table: "Prices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInvoiceItems_CustomerInvoices_InvoiceId",
                table: "CustomerInvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Products_ProductId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Products_ProductId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ProductId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_ProductId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInvoiceItems_InvoiceId",
                table: "CustomerInvoiceItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "CustomerInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "CustomerInvoiceItems");

            migrationBuilder.AddColumn<long>(
                name: "CustomerID",
                table: "Customers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TotalAmount",
                table: "CustomerInvoices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "CustomerInvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "CustomerInvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductReference",
                table: "Prices",
                column: "ProductReference");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuReference",
                table: "MenuItems",
                column: "MenuReference");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ProductReference",
                table: "MenuItems",
                column: "ProductReference");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoiceItems_InvoiceReference",
                table: "CustomerInvoiceItems",
                column: "InvoiceReference");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInvoiceItems_CustomerInvoices_InvoiceReference",
                table: "CustomerInvoiceItems",
                column: "InvoiceReference",
                principalTable: "CustomerInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuReference",
                table: "MenuItems",
                column: "MenuReference",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Products_ProductReference",
                table: "MenuItems",
                column: "ProductReference",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Products_ProductReference",
                table: "Prices",
                column: "ProductReference",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
