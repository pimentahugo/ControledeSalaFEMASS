using System.ComponentModel.DataAnnotations;

namespace ControledeSalaFEMASS.Domain.Entities;
public class Sala
{
    public int Id { get; set; }
    [MaxLength(1)]
    public string Bloco { get; set; } = string.Empty;
    public long Numero { get; set; }
    public int CapacidadeMaxima { get; set; }
    public bool PossuiLaboratorio { get; set; } = false;
    public bool PossuiArCondicionado { get; set; } = false;
    public bool PossuiLoucaDigital { get; set; } = false;
    public List<Indisponibilidade>? Indisponibilidades { get; set; } = [];
    public List<AlocacaoSala>? Alocacoes { get; set; } = [];
}