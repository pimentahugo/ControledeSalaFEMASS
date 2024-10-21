using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControledeSalaFEMASS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NecessitaLaboratorio = table.Column<bool>(type: "bit", nullable: false),
                    NecessitaArCondicionado = table.Column<bool>(type: "bit", nullable: false),
                    NecessitaLoucaDigital = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bloco = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Numero = table.Column<long>(type: "bigint", nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "int", nullable: false),
                    PossuiLaboratorio = table.Column<bool>(type: "bit", nullable: false),
                    PossuiArCondicionado = table.Column<bool>(type: "bit", nullable: false),
                    PossuiLoucaDigital = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoTurma = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Professor = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false),
                    QuantidadeAlunos = table.Column<int>(type: "int", nullable: true),
                    CodigoHorario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indisponibilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    Tempo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indisponibilidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indisponibilidades_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlocacoesSala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    Tempo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlocacoesSala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlocacoesSala_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlocacoesSala_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlocacoesSala_SalaId",
                table: "AlocacoesSala",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlocacoesSala_TurmaId",
                table: "AlocacoesSala",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Indisponibilidades_SalaId",
                table: "Indisponibilidades",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_DisciplinaId",
                table: "Turmas",
                column: "DisciplinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlocacoesSala");

            migrationBuilder.DropTable(
                name: "Indisponibilidades");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Disciplinas");
        }
    }
}
