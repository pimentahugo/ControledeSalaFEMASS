using ControledeSalaFEMASS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ControledeSalaFEMASS.Domain.Dtos;
public class TurmaDto
{
    public string CodigoTurma { get; set; } = string.Empty;
    public string Professor { get; set; } = string.Empty;
    public string NomeDisciplina { get; set; }
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
}