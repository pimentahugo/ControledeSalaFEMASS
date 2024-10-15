using AutoMapper;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Criar;
public class CriarSalaCommandHandler : IRequestHandler<CriarSalaCommand, SalaDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CriarSalaCommandHandler(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SalaDto> Handle(CriarSalaCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);

        var sala = _mapper.Map<Domain.Entities.Sala>(request);

        _context.Salas.Add(sala);
        await _context.SaveChangesAsync();

        return _mapper.Map<SalaDto>(sala);
    }

    private async Task Validate(CriarSalaCommand request)
    {
        var validator = new CriarSalaCommandValidator();

        var result = validator.Validate(request);

        var existeSala = await _context.Salas.AnyAsync(p => p.Bloco.Equals(request.Bloco) && p.Numero.Equals(request.Numero));

        if (existeSala)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("", "Já existe uma sala cadastrada com esse bloco e número"));
        }

        if(!result.IsValid)
        {
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}