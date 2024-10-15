using AutoMapper;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
public class ObterSalaPorIdQueryHandler : IRequestHandler<ObterSalaPorIdQuery, SalaDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ObterSalaPorIdQueryHandler(
        AppDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SalaDto> Handle(ObterSalaPorIdQuery request, CancellationToken cancellationToken)
    {
        var sala = await _context.Salas
            .Include(p => p.Alocacoes!)
                .ThenInclude(p => p.Turma)
                .ThenInclude(e => e.Disciplina)
            .Include(p => p.Indisponibilidades)
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if(sala is null)
        {
            throw new NotFoundException("Sala nao encontrada na base de dados");
        }

        return _mapper.Map<SalaDto>(sala);
    }
}