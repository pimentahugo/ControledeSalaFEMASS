using AutoMapper;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
public class GetSalaByIdQueryHandler : IRequestHandler<GetSalaByIdQuery, GetSalaByIdResponse>
{
    private readonly ISalaRepository _context;
    private readonly IMapper _mapper;

    public GetSalaByIdQueryHandler(
        ISalaRepository context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetSalaByIdResponse> Handle(GetSalaByIdQuery request, CancellationToken cancellationToken)
    {
        var sala = await _context.GetById(request.Id);

        if(sala is null)
        {
            throw new NotFoundException("Sala nao encontrada na base de dados");
        }

        return _mapper.Map<GetSalaByIdResponse>(sala);
    }
}