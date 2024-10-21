using ControledeSalaFEMASS.Domain.Entities;

namespace ControledeSalaFEMASS.Domain.Repositories;
public interface IDisciplinaRepository
{
    Task<IList<Disciplina>> GetAll();
    Task<Disciplina?> GetById(long disciplinaId);
    Task Add(Disciplina disciplina); 
    void Update(Disciplina disciplina); 
}
