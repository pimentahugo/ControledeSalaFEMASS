using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
public class CriarIndisponibilidadeCommand : IRequest<bool>
{
    public int SalaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public int CodigoHorario { get; set; }
}