using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.DeletarAlocacao;
public class DeletarAlocacaoTurmaCommandHandler : IRequestHandler<DeletarAlocacaoTurmaCommand>
{
    private readonly AppDbContext _context;

    public DeletarAlocacaoTurmaCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletarAlocacaoTurmaCommand request, CancellationToken cancellationToken)
    {
        var alocacao = await _context.AlocacoesSala.FindAsync(request.AlocacaoId);

        if (alocacao == null)
        {
            throw new NotFoundException("A alocacao informada nao foi encontrada");
        }

        _context.AlocacoesSala.Remove(alocacao);
        await _context.SaveChangesAsync();
    }
}