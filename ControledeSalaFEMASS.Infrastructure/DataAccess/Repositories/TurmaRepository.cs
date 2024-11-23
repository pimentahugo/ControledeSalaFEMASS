using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Infrastructure.DataAccess.Repositories;
internal class TurmaRepository : ITurmaRepository
{
    private readonly AppDbContext _context;

    public TurmaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Turma>> GetAll()
    {
        return await _context.Turmas
				.Include(t => t.Disciplina)
				.Include(t => t.Alocacoes)
				.Include(t => t.TurmasGradeAntiga)
				.Where(t => !t.TurmaId.HasValue)
				.ToListAsync();;
    }

    public async Task<IList<AlocacaoSala>> GetAlocacoesByTurma(long turmaId)
    {
        return await _context.AlocacoesSala.Where(aloc => aloc.TurmaId == turmaId).ToListAsync();
    }

    public async Task<Turma?> GetById(long turmaId)
    {
        return await _context.Turmas
            .Include(t => t.Disciplina)
            .Include(t => t.Alocacoes)
			.Include(t => t.TurmasGradeAntiga)
            .FirstOrDefaultAsync(turma => turma.Id == turmaId);
    }

    public void Update(Turma turma)
    {
        _context.Turmas.Update(turma);
    }

    public async Task Add(Turma turma)
    {
        await _context.Turmas.AddAsync(turma);
    }

	public async Task<bool> ExistsTurmaWithCodigo(int codigoTurma)
    {
		return await _context.Turmas.AnyAsync(turma => turma.Id == codigoTurma);
    }

    public async Task AddRange(List<Turma> turmas)
    {
        await _context.Turmas.AddRangeAsync(turmas);
    }

    public async Task<bool> ExistsTurmaAgrupada()
    {
        return await _context.Turmas.GroupBy(t => new { t.Professor, t.CodigoHorario }).AnyAsync(g => g.Count() > 1);
    }

    public async Task<IList<Turma>> GetTurmasAgrupadas()
    {
        var turmas = await GetAll();

        var resultado = turmas
        .GroupBy(t => new { t.Professor, t.CodigoHorario })
        .Where(g => g.Count() > 1)
        .SelectMany(g => g)
        .OrderBy(t => t.Professor)
        .ThenByDescending(t => t.QuantidadeAlunos)
        .ToList();

        return resultado;
    }

    public void LimparSemestre()
    {
        _context.Salas.RemoveRange(_context.Salas);
        _context.Turmas.RemoveRange(_context.Turmas);
    }
}