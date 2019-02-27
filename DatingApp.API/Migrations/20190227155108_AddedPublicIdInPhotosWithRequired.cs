using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingAPP.API.Migrations
{
    public partial class AddedPublicIdInPhotosWithRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                schema: "sch_dta",
                table: "t_Photo",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                schema: "sch_dta",
                table: "t_Photo");
        }
    }
}
