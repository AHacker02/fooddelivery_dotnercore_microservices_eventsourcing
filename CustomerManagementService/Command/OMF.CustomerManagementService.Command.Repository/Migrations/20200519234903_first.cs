using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMF.CustomerManagementService.Command.Repository.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "tblCustomer",
                table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    LastName = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    Email = table.Column<string>(maxLength: 225, nullable: false, defaultValueSql: "('')"),
                    MobileNumber = table.Column<string>(maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    Password = table.Column<byte[]>(nullable: false),
                    PasswordKey = table.Column<byte[]>(nullable: false),
                    Address = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    Active = table.Column<bool>(nullable: false),
                    RecordTimeStamp = table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "((0))"),
                    RecordTimeStampCreated =
                        table.Column<DateTime>("datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table => { table.PrimaryKey("PK_tblCustomer", x => x.ID); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "tblCustomer");
        }
    }
}