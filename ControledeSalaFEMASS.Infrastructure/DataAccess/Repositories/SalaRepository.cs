using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Entities;
using ControledeSalaFEMASS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControledeSalaFEMASS.Infrastructure.DataAccess.Repositories;
public class SalaRepository : ISalaRepository
{
    private readonly AppDbContext _context;

    public SalaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IList<Sala>> GetAll()
    {
        return await _context.Salas.ToListAsync();
    }

    public async Task<Sala?> GetById(int salaId)
    {
        return await _context.Salas
            .Include(s => s.Indisponibilidades)
            .Include(s => s.Alocacoes)
            .FirstOrDefaultAsync(sala => sala.Id == salaId);
    }

    public async Task Add(Sala sala)
    {
        await _context.Salas.AddAsync(sala);
    }


    public void Update(Sala sala)
    {
        _context.Salas.Update(sala);
    }

    public async Task<bool> ExistsSalaWithBlocoAndNumber(string bloco, long numero)
    {
        return await _context.Salas.AnyAsync(sala => sala.Bloco.Equals(bloco) &&
                                                    sala.Numero.Equals(numero));
    }

    public async Task<IList<Sala>> GetSalasDisponiveisParaAlocacao(GetSalasDisponiveisDto request)
    {
        var salasBasicas = await _context.Salas
                    .Where(sala => sala.CapacidadeMaxima >= request.Turma.QuantidadeAlunos &&
                      !sala.Alocacoes.Any(aloc => aloc.DiaSemana == request.DiaSemana && aloc.Tempo == request.TempoSala) &&
                      !sala.Indisponibilidades.Any(indis => indis.DiaSemana == request.DiaSemana && indis.Tempo == request.TempoSala))
                    .ToListAsync();

        var salasDisponiveis = salasBasicas
           .Where(sala => (!request.Turma.Disciplina.NecessitaLaboratorio || sala.PossuiLaboratorio) &&
                          (!request.Turma.Disciplina.NecessitaArCondicionado || sala.PossuiArCondicionado) &&
                          (!request.Turma.Disciplina.NecessitaLoucaDigital || sala.PossuiLoucaDigital))
           .ToList();

        return salasDisponiveis;
    }

    public async Task AddAlocacao(AlocacaoSala alocacaoSala)
    {
        await _context.AlocacoesSala.AddAsync(alocacaoSala);
    }

    public void DeleteAlocacao(AlocacaoSala alocacao)
    {
        _context.AlocacoesSala.Remove(alocacao);
    }
}