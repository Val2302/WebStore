using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class M4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if ( migrationBuilder is null )
            {
                throw new ArgumentNullException( nameof( migrationBuilder ) );
            }


            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if ( migrationBuilder is null )
            {
                throw new ArgumentNullException( nameof( migrationBuilder ) );
            }

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Products");
        }
    }
}
