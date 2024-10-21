using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.DeletarAlocacao;
public class DeletarAlocacaoTurmaCommand : IRequest
{
    public int AlocacaoId { get; set; }
}