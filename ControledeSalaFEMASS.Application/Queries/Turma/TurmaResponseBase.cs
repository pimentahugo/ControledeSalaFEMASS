using ControledeSalaFEMASS.Application.Queries.Disciplina;
using ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;

namespace ControledeSalaFEMASS.Application.Queries.Turma;
public abstract class TurmaResponseBase
{
    public string CodigoTurma { get; set; } = string.Empty;
    public string Professor { get; set; } = string.Empty;
    public GetDisciplinaByIdResponse Disciplina { get; set; } = default!;
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
}