using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerInvoiceItemId",
                table: "CustomerInvoiceItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoiceItems_CustomerInvoiceItemId",
                table: "CustomerInvoiceItems",
                column: "CustomerInvoiceItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInvoiceItems_CustomerInvoiceItems_CustomerInvoiceIt~",
                table: "CustomerInvoiceItems",
                column: "CustomerInvoiceItemId",
                principalTable: "CustomerInvoiceItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInvoiceItems_CustomerInvoiceItems_CustomerInvoiceIt~",
                table: "CustomerInvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInvoiceItems_CustomerInvoiceItemId",
                table: "CustomerInvoiceItems");

            migrationBuilder.DropColumn(
                name: "CustomerInvoiceItemId",
                table: "CustomerInvoiceItems");
        }
    }
}
