using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Repositories;
using ControledeSalaFEMASS.Domain.Services;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Turmas.AlocarSalasAutomaticamente
{
	public class AlocarSalasAutomaticamenteCommandHandler : IRequestHandler<AlocarSalasAutomaticamenteCommand>
	{
		private readonly ISalaRepository _salaRepository;
		private readonly ITurmaRepository _turmaRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AlocarSalasAutomaticamenteCommandHandler(
			ISalaRepository salaRepository,
			ITurmaRepository turmaRepository,
			IUnitOfWork unitOfWork)
		{
			_salaRepository = salaRepository;
			_turmaRepository = turmaRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task Handle(AlocarSalasAutomaticamenteCommand request, CancellationToken cancellationToken)
		{
			var turmas = await _turmaRepository.GetAll();
			var salas = await _salaRepository.GetAll();

			// Ordenar as turmas por quantidade de alunos e professor
			var turmasOrdenadas = turmas
				.OrderByDescending(t => t.QuantidadeAlunos)
				.ThenBy(t => t.Professor)
				.ToList();

			var alocacoes = new List<AlocacaoSala>();

			foreach (var turma in turmasOrdenadas)
			{
				if (turma.CodigoHorario.HasValue && turma.CodigoHorario.Value != 0)
				{
					var horariosDisciplina = HorariosDisciplinasService.ObterHorariosDisciplina(turma.CodigoHorario.Value);

					foreach (var horario in horariosDisciplina)
					{
						var salasDisponiveis = await _salaRepository.GetSalasDisponiveisParaAlocacao(
							new GetSalasDisponiveisDto(turma, horario.Dia, horario.Tempo));

						salasDisponiveis = salasDisponiveis
							.Where(sala => !alocacoes.Any(a =>
								a.SalaId == sala.Id &&
								a.DiaSemana == horario.Dia &&
								a.Tempo == horario.Tempo))
							.ToList();

						var salaAlocada = salasDisponiveis
							.FirstOrDefault(s => alocacoes
								.Any(a => a.Turma.Professor == turma.Professor && a.SalaId == s.Id))
							?? salasDisponiveis
								.FirstOrDefault(s => alocacoes
									.Any(a => a.Turma.Professor == turma.Professor && a.Sala.Bloco == s.Bloco))
							?? salasDisponiveis.FirstOrDefault();

						if (salaAlocada != null)
						{
							var alocacao = new AlocacaoSala
							{
								SalaId = salaAlocada.Id,
								TurmaId = turma.Id,
								DiaSemana = horario.Dia,
								Tempo = horario.Tempo,
								Turma = turma,
								Sala = salaAlocada
							};
							alocacoes.Add(alocacao);
							await _salaRepository.AddAlocacao(alocacao);
						}
					}
				}
			}

			await _unitOfWork.Commit();
		}
	}
}