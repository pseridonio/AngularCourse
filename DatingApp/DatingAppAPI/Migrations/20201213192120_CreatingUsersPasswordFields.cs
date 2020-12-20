using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingAppAPI.Migrations
{
    public partial class CreatingUsersPasswordFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PASSWORD_HASH",
                table: "TB_USERS",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PASSWORD_SALT",
                table: "TB_USERS",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PASSWORD_HASH",
                table: "TB_USERS");

            migrationBuilder.DropColumn(
                name: "PASSWORD_SALT",
                table: "TB_USERS");
        }
    }
}
