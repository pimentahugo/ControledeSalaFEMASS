using ControledeSalaFEMASS.Domain.Repositories;

namespace ControledeSalaFEMASS.Infrastructure.DataAccess.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}