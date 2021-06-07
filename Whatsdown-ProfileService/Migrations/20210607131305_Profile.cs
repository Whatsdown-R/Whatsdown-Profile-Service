using Microsoft.EntityFrameworkCore.Migrations;

namespace Whatsdown_ProfileService.Migrations
{
    public partial class Profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    profileId = table.Column<string>(nullable: false),
                    displayName = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    profileImage = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.profileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
