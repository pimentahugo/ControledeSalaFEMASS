using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Excluir;
public class ExcluirIndisponibilidadeCommandHandler : IRequestHandler<ExcluirIndisponibilidadeCommand>
{
    private readonly AppDbContext _context;

    public ExcluirIndisponibilidadeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ExcluirIndisponibilidadeCommand request, CancellationToken cancellationToken)
    {
        var indisponibilidade = await _context.Indisponibilidades
            .FirstOrDefaultAsync(i => i.SalaId == request.SalaId && i.Id == request.IndisponibilidadeId, cancellationToken);

        if (indisponibilidade == null)
        {
            throw new NotFoundException("A indisponibilidade informada não foi encontrada em nossa base de dados");
        }

        _context.Indisponibilidades.Remove(indisponibilidade);
        await _context.SaveChangesAsync(cancellationToken);
    }
}