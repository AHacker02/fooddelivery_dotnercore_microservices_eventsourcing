using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.OrderManagementService.Command.Repository.Migrations
{
    public partial class order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFoodOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tblCustomerID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblRestaurantID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Address = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFoodOrder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblTableOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tblCustomerID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblRestaurantID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    BookingStatus = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTableOrder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblFoodOrderItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tblFoodOrderID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblMenuID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFoodOrderItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblFoodOrderMapping_tblFoodOrderID",
                        column: x => x.tblFoodOrderID,
                        principalTable: "tblFoodOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderPayment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<Guid>(nullable: false),
                    Remarks = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    tblCustomerID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    TransactionAmount = table.Column<decimal>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    TblFoodOrderId = table.Column<int>(nullable: true),
                    TblTableBookingId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblFoodOrderID",
                        column: x => x.OrderID,
                        principalTable: "tblFoodOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblTableBookingID",
                        column: x => x.OrderID,
                        principalTable: "tblTableOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblFoodOrder_TblFoodOrderId",
                        column: x => x.TblFoodOrderId,
                        principalTable: "tblFoodOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOrderPayment_tblTableOrder_TblTableBookingId1",
                        column: x => x.TblTableBookingId1,
                        principalTable: "tblTableOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTableOrderMapping",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TblTableBookingId = table.Column<int>(nullable: false),
                    TableNo = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTableOrderMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblTableOrderMapping_tblTableOrderID",
                        column: x => x.TblTableBookingId,
                        principalTable: "tblTableOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFoodOrderItem_tblFoodOrderID",
                table: "tblFoodOrderItem",
                column: "tblFoodOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderPayment_OrderID",
                table: "tblOrderPayment",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderPayment_TblFoodOrderId",
                table: "tblOrderPayment",
                column: "TblFoodOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderPayment_TblTableBookingId1",
                table: "tblOrderPayment",
                column: "TblTableBookingId1");

            migrationBuilder.CreateIndex(
                name: "IX_tblTableOrderMapping_TblTableBookingId",
                table: "tblTableOrderMapping",
                column: "TblTableBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFoodOrderItem");

            migrationBuilder.DropTable(
                name: "tblOrderPayment");

            migrationBuilder.DropTable(
                name: "tblTableOrderMapping");

            migrationBuilder.DropTable(
                name: "tblFoodOrder");

            migrationBuilder.DropTable(
                name: "tblTableOrder");
        }
    }
}
