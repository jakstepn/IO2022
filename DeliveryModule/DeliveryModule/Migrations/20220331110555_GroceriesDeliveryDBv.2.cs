using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryModule.Migrations
{
    public partial class GroceriesDeliveryDBv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourierStatusClass",
                columns: table => new
                {
                    CourierStatus = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourierStatusClass", x => x.CourierStatus);
                });

            migrationBuilder.InsertData(
                table: "CourierStatusClass",
                column: "CourierStatus",
                value: "Available");

            migrationBuilder.InsertData(
                table: "CourierStatusClass",
                column: "CourierStatus",
                value: "Busy");

            migrationBuilder.InsertData(
                table: "CourierStatusClass",
                column: "CourierStatus",
                value: "AwayFromWork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourierStatusClass");
        }
    }
}
