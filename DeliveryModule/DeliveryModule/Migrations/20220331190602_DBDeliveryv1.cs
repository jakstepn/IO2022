using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryModule.Migrations
{
    public partial class DBDeliveryv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatusClasses",
                columns: table => new
                {
                    OrderStatus = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusClasses", x => x.OrderStatus);
                });

            migrationBuilder.InsertData(
                table: "OrderStatusClasses",
                column: "OrderStatus",
                value: "WaitingForPayment");

            migrationBuilder.InsertData(
                table: "OrderStatusClasses",
                column: "OrderStatus",
                value: "OnTheWay");

            migrationBuilder.InsertData(
                table: "OrderStatusClasses",
                column: "OrderStatus",
                value: "Delivered");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStatusClasses");
        }
    }
}
