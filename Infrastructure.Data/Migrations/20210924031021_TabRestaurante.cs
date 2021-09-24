using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class TabRestaurante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(70)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(70)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(70)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(70)", nullable: false),
                    CozinhaId = table.Column<int>(type: "int", nullable: false),
                    CidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurante_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restaurante_Cozinha_CozinhaId",
                        column: x => x.CozinhaId,
                        principalTable: "Cozinha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurante_CidadeId",
                table: "Restaurante",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurante_CozinhaId",
                table: "Restaurante",
                column: "CozinhaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurante");
        }
    }
}
