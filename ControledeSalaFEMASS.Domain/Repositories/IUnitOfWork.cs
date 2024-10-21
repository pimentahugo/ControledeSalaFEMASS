namespace ControledeSalaFEMASS.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}