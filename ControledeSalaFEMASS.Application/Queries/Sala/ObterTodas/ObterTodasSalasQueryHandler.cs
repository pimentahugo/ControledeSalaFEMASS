using AutoMapper;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Queries.Sala.ObterTodas;
public class ObterTodasSalasQueryHandler : IRequestHandler<ObterTodasSalasQuery, List<SalaDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ObterTodasSalasQueryHandler(
        AppDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SalaDto>> Handle(ObterTodasSalasQuery request, CancellationToken cancellationToken)
    {
        var salas = await _context.Salas.ToListAsync(cancellationToken);

        return _mapper.Map<List<SalaDto>>(salas);
    }
}