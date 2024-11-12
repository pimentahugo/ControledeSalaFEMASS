using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.LimparSemestre;
public class LimparSemestreCommandHandler : IRequestHandler<LimparSemestreCommand>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LimparSemestreCommandHandler(
        ITurmaRepository turmaRepository, 
        IUnitOfWork unitOfWork)
    {
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(LimparSemestreCommand request, CancellationToken cancellationToken)
    {
        _turmaRepository.LimparSemestre();

        await _unitOfWork.Commit();
    }
}