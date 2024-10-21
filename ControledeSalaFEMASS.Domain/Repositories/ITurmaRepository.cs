using ControledeSalaFEMASS.Domain.Entities;

namespace ControledeSalaFEMASS.Domain.Repositories;
public interface ITurmaRepository
{
    Task<IList<Turma>> GetAll();
    Task<Turma?> GetById(long turmaId);
    Task<IList<AlocacaoSala>> GetAlocacoesByTurma(long turmaId);
    Task<bool> ExistsTurmaWithCodigo(string codigoTurma);
    Task Add(Turma turma);
    Task AddRange(List<Turma> turmas);
    void Update(Turma turma);
}