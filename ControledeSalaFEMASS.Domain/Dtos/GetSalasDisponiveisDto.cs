using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Enums;

namespace ControledeSalaFEMASS.Domain.Dtos;
public class GetSalasDisponiveisDto
{
    public GetSalasDisponiveisDto(Turma turma, DayOfWeek diaSemana, TempoSala tempoSala)
    {
        Turma = turma;
        DiaSemana = diaSemana;
        TempoSala = tempoSala;
    }

    public Turma Turma { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala TempoSala { get; set; }
}