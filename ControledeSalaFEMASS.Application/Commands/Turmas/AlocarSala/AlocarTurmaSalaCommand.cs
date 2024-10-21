using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Enums;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.AlocarSala;
public class AlocarTurmaSalaCommand : IRequest<AlocarTurmaSalaResponse>
{
    public int TurmaId { get; set; }
    public int SalaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala TempoSala { get; set; }
}