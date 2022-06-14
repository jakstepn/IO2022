using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopModule.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Addresses");

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "Id", "CurrentStatus", "OrderId", "Text" },
                values: new object[] { new Guid("ffffffff-aaaa-0000-0000-000000000000"), "Pending", new Guid("00000000-0000-0000-0000-000000000000"), "test_complaint" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AdditionalInfo", "AddressFK", "ClientAddressId", "CreationDate", "DeliveryDate", "OrderStatus" },
                values: new object[] { new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"), "additional", new Guid("eeeeeeee-dddd-ffff-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                column: "Name",
                values: new object[]
                {
                    "testingCategory",
                    "testingCategory2"
                });

            migrationBuilder.InsertData(
                table: "ShopEmployees",
                columns: new[] { "Id", "CurrentState", "Email", "EmployedSince", "LastName", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("ffffffff-cccc-cccc-0000-000000000000"), 0, "testmail", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "testowy", "tester", "000-000-000" },
                    { new Guid("ffffffff-cccc-ffff-0000-000000000000"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Price", "ProductName", "ProductTypeFK", "Quantity" },
                values: new object[,]
                {
                    { new Guid("01f2bd0b-eb14-4f3d-93e8-204217ba2a95"), true, 6m, "testName4", "testingCategory", 6 },
                    { new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"), true, 1m, "testName", "testingCategory2", 1 },
                    { new Guid("bc1131d5-d2a5-43a7-9e62-c17a3683f936"), true, 3m, "testName2", "testingCategory2", 2 },
                    { new Guid("eb364b98-4099-4227-a07a-273b5fba9383"), false, 5m, "testName3", "testingCategory2", 5 }
                });

            migrationBuilder.InsertData(
                table: "ShopManagers",
                column: "Id",
                value: new Guid("ffffffff-cccc-ffff-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Currency", "OrderFK", "ProductFK", "Quantity" },
                values: new object[] { new Guid("ffffffff-aaaa-cccc-a000-000000000000"), "USD", new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"), new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"), 1m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-aaaa-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-aaaa-cccc-a000-000000000000"));

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
                keyValue: new Guid("eb364b98-4099-4227-a07a-273b5fba9383"));

            migrationBuilder.DeleteData(
                table: "ShopEmployees",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-cccc-cccc-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "ShopManagers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-cccc-ffff-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Name",
                keyValue: "testingCategory");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"));

            migrationBuilder.DeleteData(
                table: "ShopEmployees",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-cccc-ffff-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Name",
                keyValue: "testingCategory2");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-dddd-cccc-0000-000000000000"),
                columns: new[] { "Country", "Region", "StreetNumber" },
                values: new object[] { "test", "test", "test" });

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-dddd-ffff-0000-000000000000"),
                columns: new[] { "Country", "Region", "StreetNumber" },
                values: new object[] { "test2", "test2", "test2" });
        }
    }
}
