using ControledeSalaFEMASS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Infrastructure.DataAccess;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Sala> Salas { get; set; }
    public DbSet<Indisponibilidade> Indisponibilidades { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<AlocacaoSala> AlocacoesSala { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}