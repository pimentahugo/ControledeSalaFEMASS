using ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
using ControledeSalaFEMASS.Domain.Dtos;

namespace ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
public class GetTurmaByIdResponse : TurmaResponseBase
{
	public bool IsGradeAntiga { get; set; }
}