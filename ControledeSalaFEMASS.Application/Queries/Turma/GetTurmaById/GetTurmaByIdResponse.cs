using ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
using ControledeSalaFEMASS.Domain.Dtos;

namespace ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
public class GetTurmaByIdResponse : TurmaResponseBase
{
    public List<AlocacaoDto> Alocacoes { get; set; } = [];
    public GetDisciplinaByIdResponse Disciplina { get; set; }
}