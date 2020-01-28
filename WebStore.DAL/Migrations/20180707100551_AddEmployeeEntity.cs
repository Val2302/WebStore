using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class AddEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if ( migrationBuilder is null )
            {
                throw new ArgumentNullException( nameof( migrationBuilder ) );
            }

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Age = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IsMan = table.Column<bool>(nullable: false),
                    Patronymic = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    SecretName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if ( migrationBuilder is null )
            {
                throw new ArgumentNullException( nameof( migrationBuilder ) );
            }

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
