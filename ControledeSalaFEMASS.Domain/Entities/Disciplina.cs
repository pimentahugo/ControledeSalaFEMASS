using System.ComponentModel.DataAnnotations;

namespace ControledeSalaFEMASS.Domain.Entities;
public class Disciplina
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string Nome { get; set; } = string.Empty;
    public bool NecessitaLaboratorio { get; set; } = false;
    public bool NecessitaArCondicionado { get; set; } = false;
    public bool NecessitaLoucaDigital { get; set; } = false;
}