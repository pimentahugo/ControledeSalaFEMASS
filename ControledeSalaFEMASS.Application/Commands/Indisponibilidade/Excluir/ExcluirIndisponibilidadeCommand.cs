using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Excluir;
public class ExcluirIndisponibilidadeCommand : IRequest
{
    public int SalaId { get; set; }
    public int IndisponibilidadeId { get; set; }
}