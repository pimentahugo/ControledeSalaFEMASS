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
                     .Include(p => p.Disciplina)
                     .ToListAsync();
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

    public async Task<bool> ExistsTurmaWithCodigo(string codigoTurma)
    {
        return await _context.Turmas.AnyAsync(turma => turma.CodigoTurma.Equals(codigoTurma));
    }

    public async Task AddRange(List<Turma> turmas)
    {
        await _context.Turmas.AddRangeAsync(turmas);
    }
}