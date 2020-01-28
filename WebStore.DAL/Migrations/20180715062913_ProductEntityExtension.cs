using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class ProductEntityExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if ( migrationBuilder is null )
            {
                throw new ArgumentNullException( nameof( migrationBuilder ) );
            }

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if ( migrationBuilder is null )
            {
                throw new ArgumentNullException( nameof( migrationBuilder ) );
            }

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");
        }
    }
}
