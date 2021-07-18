using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevBase.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desenvolvedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Hobby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desenvolvedores", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Desenvolvedores",
                columns: new[] { "Id", "DataNascimento", "Hobby", "Idade", "Nome", "Sexo" },
                values: new object[] { 1, new DateTime(1990, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assistir filmes", 31, "Jane Doe", 1 });

            migrationBuilder.InsertData(
                table: "Desenvolvedores",
                columns: new[] { "Id", "DataNascimento", "Hobby", "Idade", "Nome", "Sexo" },
                values: new object[] { 2, new DateTime(1992, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assistir filmes", 29, "John Doe", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desenvolvedores");
        }
    }
}
