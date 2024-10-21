using AutoMapper;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using ControledeSalaFEMASS.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.AlocarSala;
public class AlocarTurmaSalaCommandHandler : IRequestHandler<AlocarTurmaSalaCommand, AlocarTurmaSalaResponse>
{
    private readonly ISalaRepository _salaRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AlocarTurmaSalaCommandHandler(
        ISalaRepository salaRepository, 
        ITurmaRepository turmaRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _salaRepository = salaRepository;
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AlocarTurmaSalaResponse> Handle(AlocarTurmaSalaCommand request, CancellationToken cancellationToken)
    {
        var alocacao = await Validate(request);

        await _salaRepository.AddAlocacao(alocacao);

        await _unitOfWork.Commit();

        return new AlocarTurmaSalaResponse(alocacao.SalaId, alocacao.TurmaId, request.DiaSemana, request.TempoSala);
    }

    private async Task<AlocacaoSala> Validate(AlocarTurmaSalaCommand request)
    {
        var turma = await _turmaRepository.GetById(request.TurmaId);

        if (turma is null)
        {
            throw new NotFoundException("A turma informada não foi encontrada em nossa base de dados");
        }

        var sala = await _salaRepository.GetById(request.SalaId);

        if (sala == null)
        {
            throw new NotFoundException("Sala não encontrada");
        }

        var horariosDisciplina = HorariosDisciplinasService.ObterHorariosDisciplina(turma.DisciplinaId);

        var tempoAula = horariosDisciplina.First(h => h.Dia == request.DiaSemana).Tempo;

        if (!horariosDisciplina.Exists(h => h.Tempo == request.TempoSala && h.Dia == request.DiaSemana))
        {
            throw new OperationInvalidException("A disciplina informada não possui aula no horário informado.");
        }

        var alocacoesTurma = await _turmaRepository.GetAlocacoesByTurma(request.TurmaId);

        var turmaEstaAlocada = alocacoesTurma.Count >= 2;

        if (turmaEstaAlocada)
        {
            throw new OperationInvalidException("A turma informada já está alocada nos dois dias.");
        }

        var existeAlocacaoTurmaMesmoDiaEHorario = alocacoesTurma.Any(p => p.DiaSemana == request.DiaSemana &&
                                                 p.Tempo == request.TempoSala);

        if (existeAlocacaoTurmaMesmoDiaEHorario)
        {
            throw new OperationInvalidException("Já existe uma alocação feita para esta turma em uma sala. Por favor, verifique");
        }

        //var salasDisponiveis = await _context.Salas
        //   .Where(s => s.CapacidadeMaxima >= turma.QuantidadeAlunos)
        //   .Where(s => (!turma.Disciplina.NecessitaLaboratorio || s.PossuiLaboratorio) &&
        //               (!turma.Disciplina.NecessitaArCondicionado || s.PossuiArCondicionado) &&
        //               (!turma.Disciplina.NecessitaLoucaDigital || s.PossuiLoucaDigital))
        //   .Where(s => s.Alocacoes != null && !s.Alocacoes.Any(a =>
        //       a.DiaSemana == request.DiaSemana && a.Tempo == tempoAula))
        //   .Where(s => s.Indisponibilidades != null && !s.Indisponibilidades.Any(i =>
        //       i.DiaSemana == request.DiaSemana && i.Tempo == tempoAula))
        //   .ToListAsync(cancellationToken);

        var requestsSalasDisponiveis = new GetSalasDisponiveisDto(turma, request.DiaSemana, request.TempoSala);

        var salasDisponiveis = await _salaRepository.GetSalasDisponiveisParaAlocacao(requestsSalasDisponiveis);

        if (!salasDisponiveis.Any(s => s.Id == sala.Id))
        {
            throw new OperationInvalidException("A sala informada não está disponível no horário solicitado para a disciplina");
        }

        var alocacao = new AlocacaoSala
        {
            SalaId = sala.Id,
            TurmaId = turma.Id,
            DiaSemana = request.DiaSemana,
            Tempo = tempoAula
        };

        return alocacao;
    }
}