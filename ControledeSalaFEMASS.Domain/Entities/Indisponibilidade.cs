namespace ControledeSalaFEMASS.Domain.Entities;
public class Indisponibilidade
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public Sala Sala { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public int CodigoHorario { get; set; }
}