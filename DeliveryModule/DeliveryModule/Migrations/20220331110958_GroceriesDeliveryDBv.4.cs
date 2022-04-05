using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryModule.Migrations
{
    public partial class GroceriesDeliveryDBv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourierStatusClass",
                table: "CourierStatusClass");

            migrationBuilder.RenameTable(
                name: "CourierStatusClass",
                newName: "CouriersStatusClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CouriersStatusClasses",
                table: "CouriersStatusClasses",
                column: "CourierStatus");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MyProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Clientid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_Clientid",
                        column: x => x.Clientid,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentOrderId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStateCourierStatus = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Couriers_CouriersStatusClasses_CurrentStateCourierStatus",
                        column: x => x.CurrentStateCourierStatus,
                        principalTable: "CouriersStatusClasses",
                        principalColumn: "CourierStatus",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Couriers_Orders_CurrentOrderId",
                        column: x => x.CurrentOrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_CurrentOrderId",
                table: "Couriers",
                column: "CurrentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_CurrentStateCourierStatus",
                table: "Couriers",
                column: "CurrentStateCourierStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Clientid",
                table: "Orders",
                column: "Clientid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CouriersStatusClasses",
                table: "CouriersStatusClasses");

            migrationBuilder.RenameTable(
                name: "CouriersStatusClasses",
                newName: "CourierStatusClass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourierStatusClass",
                table: "CourierStatusClass",
                column: "CourierStatus");
        }
    }
}
