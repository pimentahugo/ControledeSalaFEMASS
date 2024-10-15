using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Application.Commands.Importacao.Turma;
public class ImportarTurmasCommandHandler : IRequestHandler<ImportarTurmasCommand>
{
    private readonly AppDbContext _context;

    public ImportarTurmasCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ImportarTurmasCommand request, CancellationToken cancellationToken)
    {
        foreach (var turmaDto in request.Turmas)
        {
            var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Nome == turmaDto.Disciplina, cancellationToken);

            if (disciplina == null)
            {
                disciplina = new Disciplina { Nome = turmaDto.Disciplina };
                _context.Disciplinas.Add(disciplina);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var turma = new Domain.Entities.Turma
            {
                CodigoTurma = turmaDto.CodigoTurma,
                Professor = turmaDto.Professor,
                DisciplinaId = disciplina.Id,
                QuantidadeAlunos = turmaDto.QuantidadeAlunos,
                CodigoHorario = turmaDto.CodigoHorario
            };

            _context.Turmas.Add(turma);
        }

        await _context.SaveChangesAsync();
    }
}