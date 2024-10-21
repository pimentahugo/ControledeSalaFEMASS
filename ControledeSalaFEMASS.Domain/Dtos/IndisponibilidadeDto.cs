using ControledeSalaFEMASS.Domain.Enums;

namespace ControledeSalaFEMASS.Domain.Dtos;
public class IndisponibilidadeDto
{
    public long Id { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TempoSala Tempo { get; set; }
}