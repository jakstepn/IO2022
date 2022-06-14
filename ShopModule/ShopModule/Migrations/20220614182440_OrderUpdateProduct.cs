using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopModule.Migrations
{
    public partial class OrderUpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("01f2bd0b-eb14-4f3d-93e8-204217ba2a95"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bc1131d5-d2a5-43a7-9e62-c17a3683f936"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eb364b98-4099-4227-a07a-273b5fba9383"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Price", "ProductName", "ProductTypeFK", "Quantity" },
                values: new object[,]
                {
                    { new Guid("9cb92278-d017-40d1-970a-48bbb5df60a5"), true, 1m, "testName", "testingCategory2", 1 },
                    { new Guid("f8eca3cd-9f2a-491c-9067-a03cc10be615"), true, 3m, "testName2", "testingCategory2", 2 },
                    { new Guid("7f270309-477d-4ef5-8bea-f50663551c72"), false, 5m, "testName3", "testingCategory2", 5 },
                    { new Guid("8802e949-5e40-4052-aaf3-92ebb560888e"), true, 6m, "testName4", "testingCategory", 6 }
                });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-aaaa-cccc-a000-000000000000"),
                column: "ProductFK",
                value: new Guid("9cb92278-d017-40d1-970a-48bbb5df60a5"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7f270309-477d-4ef5-8bea-f50663551c72"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8802e949-5e40-4052-aaf3-92ebb560888e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9cb92278-d017-40d1-970a-48bbb5df60a5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f8eca3cd-9f2a-491c-9067-a03cc10be615"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Price", "ProductName", "ProductTypeFK", "Quantity" },
                values: new object[,]
                {
                    { new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"), true, 1m, "testName", "testingCategory2", 1 },
                    { new Guid("bc1131d5-d2a5-43a7-9e62-c17a3683f936"), true, 3m, "testName2", "testingCategory2", 2 },
                    { new Guid("eb364b98-4099-4227-a07a-273b5fba9383"), false, 5m, "testName3", "testingCategory2", 5 },
                    { new Guid("01f2bd0b-eb14-4f3d-93e8-204217ba2a95"), true, 6m, "testName4", "testingCategory", 6 }
                });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-aaaa-cccc-a000-000000000000"),
                column: "ProductFK",
                value: new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"));
        }
    }
}
