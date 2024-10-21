using ControledeSalaFEMASS.Application.Queries.Sala.GetAll;
using ControledeSalaFEMASS.Domain.Enums;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Turma.ObterSalasDisponiveis;
public class ObterSalasDisponiveisQuery : IRequest<List<GetAllSalasResponse>>
{
    public int TurmaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala TempoAula { get; set; }
}