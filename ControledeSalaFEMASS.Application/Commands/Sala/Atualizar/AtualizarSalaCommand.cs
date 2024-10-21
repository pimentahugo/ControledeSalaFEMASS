using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
public class AtualizarSalaCommand : SalaCommandBase, IRequest
{
    public int Id { get; set; }
}