using AutoMapper;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Queries.Turma.ObterTurmas;
public class ObterTurmasQueryHandler : IRequestHandler<ObterTurmasQuery, List<TurmaDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ObterTurmasQueryHandler(AppDbContext appDbContext, IMapper mapper)
    {
        _context = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<TurmaDto>> Handle(ObterTurmasQuery request, CancellationToken cancellationToken)
    {
        var turmas = await _context.Turmas
            .Include(p => p.Disciplina)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TurmaDto>>(turmas);
    }
}