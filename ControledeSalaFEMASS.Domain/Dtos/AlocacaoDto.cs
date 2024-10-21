using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Enums;

namespace ControledeSalaFEMASS.Domain.Dtos;
public class AlocacaoDto
{
    //public Sala Sala { get; set; }
    //public TurmaDto Turma { get; set; } = default!;
    public long Id { get; set; }
    public long SalaId { get; set; }
    public int TurmaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala Tempo { get; set; }
}