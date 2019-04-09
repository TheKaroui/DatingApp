using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingAPP.API.Migrations
{
    public partial class AddingSqlServerSqLiteTogetherInMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_User_Username",
                schema: "sch_dta",
                table: "t_User");

            migrationBuilder.AddColumn<int>(
                name: "SqlServerSqLiteTogetherInMigrations",
                schema: "sch_dta",
                table: "t_Value",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_User_Username",
                schema: "sch_dta",
                table: "t_User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_User_Username",
                schema: "sch_dta",
                table: "t_User");

            migrationBuilder.DropColumn(
                name: "SqlServerSqLiteTogetherInMigrations",
                schema: "sch_dta",
                table: "t_Value");

            migrationBuilder.CreateIndex(
                name: "IX_t_User_Username",
                schema: "sch_dta",
                table: "t_User",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }
    }
}
