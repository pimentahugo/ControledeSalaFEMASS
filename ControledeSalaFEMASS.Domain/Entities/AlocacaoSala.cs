using ControledeSalaFEMASS.Domain.Enums;

namespace ControledeSalaFEMASS.Domain.Entities;
public class AlocacaoSala
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public Sala Sala { get; set; }
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala Tempo { get; set; }
}