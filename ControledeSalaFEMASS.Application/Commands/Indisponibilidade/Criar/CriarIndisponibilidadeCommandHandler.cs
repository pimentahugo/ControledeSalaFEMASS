using AutoMapper;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
public class CriarIndisponibilidadeCommandHandler : IRequestHandler<CriarIndisponibilidadeCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CriarIndisponibilidadeCommandHandler(
        AppDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CriarIndisponibilidadeCommand request, CancellationToken cancellationToken)
    {
        var indisponibilidade = _mapper.Map<Domain.Entities.Indisponibilidade>(request);

        _context.Indisponibilidades.Add(indisponibilidade);
        await _context.SaveChangesAsync();

        return true;
    }
}