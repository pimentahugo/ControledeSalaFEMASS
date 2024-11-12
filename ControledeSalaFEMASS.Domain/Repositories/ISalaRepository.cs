using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Entities;

namespace ControledeSalaFEMASS.Domain.Repositories;
public interface ISalaRepository
{
    Task<IList<Sala>> GetAll();
    Task<Sala?> GetById(int salaId);
    Task<IList<Sala>> GetSalasDisponiveisParaAlocacao(GetSalasDisponiveisDto request);
    Task Add(Sala sala);
    Task AddAlocacao(AlocacaoSala alocacaoSala);
    void DeleteAlocacao(AlocacaoSala alocacao);
    void DeleteAlocacoes(List<AlocacaoSala> alocacoes);
    Task<bool> ExistsSalaWithBlocoAndNumber(string bloco, long numero);
    void Update(Sala sala);
}