using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OMF.RestaurantService.Query.Repository.Migrations
{
    public partial class restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCuisine",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuisine = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(nullable: false),
                    RecordTimeStampCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCuisine", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblLocation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    Y = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(nullable: false),
                    RecordTimeStampCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLocation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblMenu",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    tblCuisineID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStamp = table.Column<DateTime>(nullable: false),
                    RecordTimeStampCreated = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMenu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblMenu_tblCuisineID",
                        column: x => x.tblCuisineID,
                        principalTable: "tblCuisine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblRestaurant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    tblLocationID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    ContactNo = table.Column<string>(maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    ModifiedTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    CreatedRecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    Address = table.Column<string>(unicode: false, nullable: true),
                    Website = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    OpeningTime = table.Column<string>(maxLength: 5, nullable: false, defaultValueSql: "('')"),
                    CloseTime = table.Column<string>(maxLength: 5, nullable: false, defaultValueSql: "('')"),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRestaurant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRestaurant_tblLocationID",
                        column: x => x.tblLocationID,
                        principalTable: "tblLocation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOffer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tblRestaurantID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    tblMenuID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "decimal", nullable: false, defaultValueSql: "((0))"),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    Discount = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>(nullable: false),
                    RecordTimeStampCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOffer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblOffer_tblMenuID",
                        column: x => x.tblMenuID,
                        principalTable: "tblMenu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOffer_tblRestaurantID",
                        column: x => x.tblRestaurantID,
                        principalTable: "tblRestaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblRestaurantDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tblRestaurantID = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    TableCount = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    TableCapacity = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Active = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<int>(nullable: false),
                    UserModified = table.Column<int>(nullable: false),
                    ModifiedTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))"),
                    CreatedRecordTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRestaurantDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRestaurantDetails_tblRestaurantID",
                        column: x => x.tblRestaurantID,
                        principalTable: "tblRestaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__tblLocat__3BD0198414754610",
                table: "tblLocation",
                column: "X",
                unique: true,
                filter: "[X] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__tblLocat__3BD01987EC697B94",
                table: "tblLocation",
                column: "Y",
                unique: true,
                filter: "[Y] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblMenu_tblCuisineID",
                table: "tblMenu",
                column: "tblCuisineID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOffer_tblMenuID",
                table: "tblOffer",
                column: "tblMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOffer_tblRestaurantID",
                table: "tblOffer",
                column: "tblRestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRestaurant_tblLocationID",
                table: "tblRestaurant",
                column: "tblLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRestaurantDetails_tblRestaurantID",
                table: "tblRestaurantDetails",
                column: "tblRestaurantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOffer");

            migrationBuilder.DropTable(
                name: "tblRestaurantDetails");

            migrationBuilder.DropTable(
                name: "tblMenu");

            migrationBuilder.DropTable(
                name: "tblRestaurant");

            migrationBuilder.DropTable(
                name: "tblCuisine");

            migrationBuilder.DropTable(
                name: "tblLocation");
        }
    }
}
