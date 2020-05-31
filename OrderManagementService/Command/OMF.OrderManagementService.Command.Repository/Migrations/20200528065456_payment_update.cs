using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.OrderManagementService.Command.Repository.Migrations
{
    public partial class payment_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFoodOrderMapping_tblFoodOrderID",
                table: "tblFoodOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrderPayment_tblFoodOrderID",
                table: "tblOrderPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrderPayment_tblTableBookingID",
                table: "tblOrderPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrderPayment_tblFoodOrder_TblFoodOrderId",
                table: "tblOrderPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrderPayment_tblTableOrder_TblTableBookingId1",
                table: "tblOrderPayment");

            migrationBuilder.DropIndex(
                name: "IX_tblOrderPayment_OrderID",
                table: "tblOrderPayment");

            migrationBuilder.DropIndex(
                name: "IX_tblOrderPayment_TblFoodOrderId",
                table: "tblOrderPayment");

            migrationBuilder.DropIndex(
                name: "IX_tblOrderPayment_TblTableBookingId1",
                table: "tblOrderPayment");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "tblOrderPayment");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "tblOrderPayment");

            migrationBuilder.DropColumn(
                name: "TblFoodOrderId",
                table: "tblOrderPayment");

            migrationBuilder.DropColumn(
                name: "TblTableBookingId1",
                table: "tblOrderPayment");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "tblTableOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "tblFoodOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tblFoodOrderItem_tblFoodOrder_tblFoodOrderID",
                table: "tblFoodOrderItem",
                column: "tblFoodOrderID",
                principalTable: "tblFoodOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFoodOrderItem_tblFoodOrder_tblFoodOrderID",
                table: "tblFoodOrderItem");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "tblTableOrder");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "tblFoodOrder");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "tblOrderPayment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "tblOrderPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TblFoodOrderId",
                table: "tblOrderPayment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TblTableBookingId1",
                table: "tblOrderPayment",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_tblFoodOrderMapping_tblFoodOrderID",
                table: "tblFoodOrderItem",
                column: "tblFoodOrderID",
                principalTable: "tblFoodOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrderPayment_tblFoodOrderID",
                table: "tblOrderPayment",
                column: "OrderID",
                principalTable: "tblFoodOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrderPayment_tblTableBookingID",
                table: "tblOrderPayment",
                column: "OrderID",
                principalTable: "tblTableOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrderPayment_tblFoodOrder_TblFoodOrderId",
                table: "tblOrderPayment",
                column: "TblFoodOrderId",
                principalTable: "tblFoodOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrderPayment_tblTableOrder_TblTableBookingId1",
                table: "tblOrderPayment",
                column: "TblTableBookingId1",
                principalTable: "tblTableOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
