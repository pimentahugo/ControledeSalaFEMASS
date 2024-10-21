using AutoMapper;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
public class AtualizarSalaCommandHandler : IRequestHandler<AtualizarSalaCommand>
{
    private readonly ISalaRepository _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AtualizarSalaCommandHandler(
        ISalaRepository context, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(AtualizarSalaCommand request, CancellationToken cancellationToken)
    {  
        var sala = await _context.GetById(request.Id);

        if (sala is null)
        {
            throw new NotFoundException("Sala não encontrada na base de dados");
        }

        _mapper.Map(request, sala);

        _context.Update(sala);

        await _unitOfWork.Commit();
    }
}