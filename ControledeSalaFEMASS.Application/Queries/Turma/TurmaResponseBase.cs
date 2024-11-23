using ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
using ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
using ControledeSalaFEMASS.Domain.Dtos;

namespace ControledeSalaFEMASS.Application.Queries.Turma;
public abstract class TurmaResponseBase
{
    public int Id { get; set; }
    public string Professor { get; set; } = string.Empty;
    public int? QuantidadeAlunos { get; set; }
	public int? TotalQuantidadeAlunosAgrupados { get; set; }
    public int? CodigoHorario { get; set; }
	public List<GetTurmaByIdResponse?> TurmasGrandeAntiga { get; set; } = [];
	public List<AlocacaoDto> Alocacoes { get; set; } = [];
	public GetDisciplinaByIdResponse Disciplina { get; set; } = default!;
}