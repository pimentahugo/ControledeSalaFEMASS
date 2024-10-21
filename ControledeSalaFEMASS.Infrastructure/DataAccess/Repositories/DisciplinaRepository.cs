using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Infrastructure.DataAccess.Repositories;
public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly AppDbContext _context;

    public DisciplinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Disciplina>> GetAll()
    {
        return await _context.Disciplinas.ToListAsync();
    }

    public async Task<Disciplina?> GetById(long disciplinaId)
    {
        return await _context.Disciplinas.FirstOrDefaultAsync(disc => disc.Id == disciplinaId);
    }

    public async Task Add(Disciplina disciplina)
    {
        await _context.Disciplinas.AddAsync(disciplina);
    }

    public void Update(Disciplina disciplina)
    {
        _context.Disciplinas.Update(disciplina);
    }
}