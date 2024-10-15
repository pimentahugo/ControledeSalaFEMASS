using AutoMapper;
using ControledeSalaFEMASS.Application.Commands.Sala.Criar;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
public class AtualizarSalaCommandHandler : IRequestHandler<AtualizarSalaCommand>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AtualizarSalaCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(AtualizarSalaCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);
        
        var sala = await _context.Salas.FindAsync(request.Id);

        if (sala == null)
        {
            throw new NotFoundException("Sala não encontrada na base de dados");
        }

        sala = _mapper.Map<Domain.Entities.Sala>(request);

        _context.Salas.Update(sala);

        await _context.SaveChangesAsync(cancellationToken);
    }
    private async Task Validate(AtualizarSalaCommand request)
    {
        var validator = new AtualizarSalaCommandValidator();

        var result = validator.Validate(request);

        var existeSala = await _context.Salas.AnyAsync(p => p.Id != request.Id &&
            p.Bloco.Equals(request.Bloco) &&
            p.Numero.Equals(request.Numero));

        if (existeSala)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("", "Já existe uma sala cadastrada com esse bloco e número"));
        }

        if (!result.IsValid)
        {
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}