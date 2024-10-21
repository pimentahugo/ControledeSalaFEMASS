using AutoMapper;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Disciplina.Atualizar;
public class AtualizarDisciplinaCommandHandler : IRequestHandler<AtualizarDisciplinaCommand>
{
    private readonly IDisciplinaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AtualizarDisciplinaCommandHandler(
        IDisciplinaRepository repository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(AtualizarDisciplinaCommand request, CancellationToken cancellationToken)
    {
        var disciplina = await _repository.GetById(request.Id);

        if(disciplina is null)
        {
            throw new NotFoundException("A disciplina informada não foi encontrada");
        }

        _mapper.Map(request, disciplina);

        _repository.Update(disciplina);

        await _unitOfWork.Commit();
    }
}