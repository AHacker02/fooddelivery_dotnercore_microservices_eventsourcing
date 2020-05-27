using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.RestaurantService.Query.Repository.Migrations
{
    public partial class restaurantupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "tblRestaurant",
                type: "decimal(3,1)",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "tblRestaurant",
                type: "decimal(18,2)",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "tblRestaurant",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "tblRestaurant",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValueSql: "((0))");
        }
    }
}
