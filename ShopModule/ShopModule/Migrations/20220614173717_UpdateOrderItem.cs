using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopModule.Migrations
{
    public partial class UpdateOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedPayment",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GrossPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "OrderItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "OrderItems",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedPayment",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossPrice",
                table: "OrderItems",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "OrderItems",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
