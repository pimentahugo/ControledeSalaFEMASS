using ControledeSalaFEMASS.Domain.Enums;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.AlocarSala;
public class AlocarTurmaSalaResponse
{
    public AlocarTurmaSalaResponse(long salaId, long turmaId, DayOfWeek diaSemana, TempoSala tempo)
    {
        SalaId = salaId;
        TurmaId = turmaId;
        DiaSemana = diaSemana;
        Tempo = tempo;
    }

    public long SalaId { get; set; }
    public long TurmaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala Tempo { get; set; }
}