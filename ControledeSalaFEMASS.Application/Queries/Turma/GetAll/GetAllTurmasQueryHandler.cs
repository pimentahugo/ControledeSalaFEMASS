using AutoMapper;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Turma.GetAll;
public class GetAllTurmasQueryHandler : IRequestHandler<GetAllTurmasQuery, List<GetAllTurmasResponse>>
{
    private readonly ITurmaRepository _context;
    private readonly IMapper _mapper;

    public GetAllTurmasQueryHandler(
        ITurmaRepository appDbContext, IMapper mapper)
    {
        _context = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<GetAllTurmasResponse>> Handle(GetAllTurmasQuery request, CancellationToken cancellationToken)
    {
        var turmas = await _context.GetAll();


        return _mapper.Map<List<GetAllTurmasResponse>>(turmas);
    }
}