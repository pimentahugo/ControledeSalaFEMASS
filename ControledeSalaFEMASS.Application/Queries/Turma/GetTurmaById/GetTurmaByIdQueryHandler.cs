using AutoMapper;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
public class GetTurmaByIdQueryHandler : IRequestHandler<GetTurmaByIdQuery, GetTurmaByIdResponse>
{
    private readonly ITurmaRepository _context;
    private readonly IMapper _mapper;

    public GetTurmaByIdQueryHandler(
        ITurmaRepository context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTurmaByIdResponse> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
    {
        var turma = await _context.GetById(request.TurmaId);

        if(turma is null)
        {
            throw new NotFoundException("A turma informada não foi encontrada");
        }

        return _mapper.Map<GetTurmaByIdResponse>(turma);
    }
}