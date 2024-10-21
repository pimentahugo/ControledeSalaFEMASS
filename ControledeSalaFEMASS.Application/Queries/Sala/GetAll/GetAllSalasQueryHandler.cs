using AutoMapper;
using ControledeSalaFEMASS.Application.Queries.Sala.GetAll;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Sala.GetAll;
public class GetAllSalasQueryHandler : IRequestHandler<GetAllSalasQuery, List<GetAllSalasResponse>>
{
    private readonly ISalaRepository _context;
    private readonly IMapper _mapper;

    public GetAllSalasQueryHandler(
        ISalaRepository context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetAllSalasResponse>> Handle(GetAllSalasQuery request, CancellationToken cancellationToken)
    {
        var salas = await _context.GetAll();

        return _mapper.Map<List<GetAllSalasResponse>>(salas);
    }
}