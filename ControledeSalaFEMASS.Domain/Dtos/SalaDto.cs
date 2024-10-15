using System.ComponentModel.DataAnnotations;

namespace ControledeSalaFEMASS.Domain.Dtos;
public class SalaDto
{
    public int Id { get; set; }
    public string Bloco { get; set; } = string.Empty;
    public long Numero { get; set; }
    public int CapacidadeMaxima { get; set; }
    public bool PossuiLaboratorio { get; set; } = false;
    public bool PossuiArCondicionado { get; set; } = false;
    public bool PossuiLoucaDigital { get; set; } = false;
    public List<AlocacaoDto>? Alocacoes { get; set; } = [];
    public List<IndisponibilidadeDto>? Indisponibilidades { get; set; } = [];
}