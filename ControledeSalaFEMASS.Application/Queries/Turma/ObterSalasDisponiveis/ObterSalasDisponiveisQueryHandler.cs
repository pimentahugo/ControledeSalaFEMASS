using AutoMapper;
using ControledeSalaFEMASS.Application.Queries.Sala.GetAll;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Enums;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using ControledeSalaFEMASS.Domain.Services;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Turma.ObterSalasDisponiveis;
public class ObterSalasDisponiveisQueryHandler : IRequestHandler<ObterSalasDisponiveisQuery, List<GetAllSalasResponse>>
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly ISalaRepository _salaRepository;
    private readonly IMapper _mapper;

    public ObterSalasDisponiveisQueryHandler(
        ITurmaRepository turmaRepository, 
        ISalaRepository salaRepository, 
        IMapper mapper)
    {
        _turmaRepository = turmaRepository;
        _salaRepository = salaRepository;
        _mapper = mapper;
    }

    public async Task<List<GetAllSalasResponse>> Handle(ObterSalasDisponiveisQuery request, CancellationToken cancellationToken)
    {
        var turma = await _turmaRepository.GetById(request.TurmaId);

        if (turma == null || !turma.CodigoHorario.HasValue)
        {
            throw new NotFoundException("Turma não encontrada ou turma sem código de horário");
        }

        var horariosDisciplina = HorariosDisciplinasService.ObterHorariosDisciplina(turma.DisciplinaId);

        Validate(request, horariosDisciplina);

        var requestSalasDisponiveis = new GetSalasDisponiveisDto(turma, request.DiaSemana, request.TempoAula);

        var salasDisponiveis = await _salaRepository.GetSalasDisponiveisParaAlocacao(requestSalasDisponiveis);

        return _mapper.Map<List<GetAllSalasResponse>>(salasDisponiveis);
    }

    private void Validate(ObterSalasDisponiveisQuery request, List<(DayOfWeek Dia, TempoSala Tempo)> horariosDisciplina)
    {
        if(!horariosDisciplina.Exists(h => h.Tempo == request.TempoAula && h.Dia == request.DiaSemana))
        {
            throw new OperationInvalidException("A disciplina informada não possui aula no horário informado.");
        }
    }
}