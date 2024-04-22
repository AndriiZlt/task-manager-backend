using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnetcore.ntier.DAL.Migrations
{
    public partial class FriendPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Friends",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Friends");
        }
    }
}
