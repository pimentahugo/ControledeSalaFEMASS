using AutoMapper;
using ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Criar;
public class CriarSalaCommandHandler : IRequestHandler<CriarSalaCommand, GetSalaByIdResponse>
{
    private readonly ISalaRepository _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CriarSalaCommandHandler(
        ISalaRepository context,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetSalaByIdResponse> Handle(CriarSalaCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);

        var sala = _mapper.Map<Domain.Entities.Sala>(request);

        await _context.Add(sala);
        await _unitOfWork.Commit();

        return _mapper.Map<GetSalaByIdResponse>(sala);
    }

    private async Task Validate(CriarSalaCommand request)
    {
        var validator = new CriarSalaCommandValidator();

        var result = validator.Validate(request);

        var existeSala = await _context.ExistsSalaWithBlocoAndNumber(request.Bloco, request.Numero);

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