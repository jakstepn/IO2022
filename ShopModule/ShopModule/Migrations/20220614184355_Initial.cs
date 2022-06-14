using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopModule.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ShopEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployedSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressFK",
                        column: x => x.AddressFK,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductTypeFK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeFK",
                        column: x => x.ProductTypeFK,
                        principalTable: "ProductTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopManagers_ShopEmployees_Id",
                        column: x => x.Id,
                        principalTable: "ShopEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderFK",
                        column: x => x.OrderFK,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductFK",
                        column: x => x.ProductFK,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Street", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("eeeeeeee-dddd-cccc-0000-000000000000"), "test", "test", "test" },
                    { new Guid("eeeeeeee-dddd-ffff-0000-000000000000"), "test2", "test2", "test2" }
                });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "Id", "CurrentStatus", "OrderId", "Text" },
                values: new object[] { new Guid("ffffffff-aaaa-0000-0000-000000000000"), "Pending", new Guid("00000000-0000-0000-0000-000000000000"), "test_complaint" });

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
                table: "Orders",
                columns: new[] { "Id", "AdditionalInfo", "AddressFK", "CreationDate", "DeliveryDate", "OrderStatus" },
                values: new object[] { new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"), "additional", new Guid("eeeeeeee-dddd-ffff-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Price", "ProductName", "ProductTypeFK", "Quantity" },
                values: new object[,]
                {
                    { new Guid("a3d268e2-065e-411a-b55c-c1eb5d34c869"), true, 6m, "testName4", "testingCategory", 6 },
                    { new Guid("eab84af7-bce2-428e-a3c3-8f375c09912f"), true, 1m, "testName", "testingCategory2", 1 },
                    { new Guid("e0e971d0-410f-4da2-aebe-c7d3b83b92e9"), true, 3m, "testName2", "testingCategory2", 2 },
                    { new Guid("f95bfbf3-e52d-4fbb-93e8-c06ad8d937bf"), false, 5m, "testName3", "testingCategory2", 5 }
                });

            migrationBuilder.InsertData(
                table: "ShopManagers",
                column: "Id",
                value: new Guid("ffffffff-cccc-ffff-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Currency", "OrderFK", "ProductFK", "Quantity" },
                values: new object[] { new Guid("ffffffff-aaaa-cccc-a000-000000000000"), "USD", new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"), new Guid("eab84af7-bce2-428e-a3c3-8f375c09912f"), 1m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderFK",
                table: "OrderItems",
                column: "OrderFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductFK",
                table: "OrderItems",
                column: "ProductFK");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressFK",
                table: "Orders",
                column: "AddressFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeFK",
                table: "Products",
                column: "ProductTypeFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ShopManagers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShopEmployees");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
