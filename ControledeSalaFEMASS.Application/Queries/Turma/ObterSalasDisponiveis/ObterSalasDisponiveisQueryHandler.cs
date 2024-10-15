using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Queries.Turma.ObterSalasDisponiveis;
public class ObterSalasDisponiveisQueryHandler : IRequestHandler<ObterSalasDisponiveisQuery, List<Domain.Entities.Sala>>
{
    private readonly AppDbContext _context;

    public ObterSalasDisponiveisQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Entities.Sala>> Handle(ObterSalasDisponiveisQuery request, CancellationToken cancellationToken)
    {
        var turma = await _context.Turmas.FindAsync(request.TurmaId);

        if (turma == null || !turma.CodigoHorario.HasValue)
        {
            throw new Exception("Turma não encontrada ou turma sem código de horário");
        }

        var disciplina = await _context.Disciplinas.FindAsync(turma.DisciplinaId);

        if (disciplina == null)
        {
            throw new Exception("Disciplina não encontrada");
        }

        var diaSemana = ObterDiaSemana(turma.CodigoHorario.Value);

        var salasDisponiveis = await _context.Salas
            .Include(sala => sala.Indisponibilidades)
            .Include(sala => sala.Alocacoes)
            .Where(s => s.CapacidadeMaxima >= turma.QuantidadeAlunos &&
                    (!disciplina.NecessitaLaboratorio || s.PossuiLaboratorio) &&
                    (!disciplina.NecessitaArCondicionado || s.PossuiArCondicionado) &&
                    (!disciplina.NecessitaLoucaDigital || s.PossuiLoucaDigital) &&
                    (!s.Indisponibilidades.Any(i => i.DiaSemana == diaSemana && i.CodigoHorario == ObterCodigoHorarioIndisponibilidade(turma.CodigoHorario.Value))) &&
                    (!s.Alocacoes.Any(a => a.Turma.CodigoHorario == turma.CodigoHorario))).ToListAsync();

        return salasDisponiveis;
    }

    private DayOfWeek ObterDiaSemana(int codigoHorarioDisciplina)
    {
        if(codigoHorarioDisciplina == 1 || codigoHorarioDisciplina == 2)
        {
            return DayOfWeek.Sunday;
        }

        if(codigoHorarioDisciplina == 3)
        {
            return DayOfWeek.Tuesday;
        }

        if(codigoHorarioDisciplina == 4)
        {
            return DayOfWeek.Wednesday;
        }

        return DayOfWeek.Thursday;
    }

    private int ObterCodigoHorarioIndisponibilidade(int codigoHorarioDisciplina)
    {
        throw new NotImplementedException();
    }
}