using ControledeSalaFEMASS.Domain.Dtos;

namespace ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
public class GetSalaByIdResponse : GetSalaBaseResponse
{
    
    public List<AlocacaoDto>? Alocacoes { get; set; } = [];
    public List<IndisponibilidadeDto>? Indisponibilidades { get; set; } = [];
}