using System.ComponentModel.DataAnnotations;

namespace ControledeSalaFEMASS.Domain.Entities;
public class Turma
{
    public int Id { get; set; }
    [MaxLength(5)]
    public string CodigoTurma { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Professor { get; set; } = string.Empty;
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
    public List<Turma>? TurmasAgrupadas { get; set; }
    public int? TotalQuantidadeAlunosAgrupados { get; set; }
    public List<AlocacaoSala>? Alocacoes { get; set; }
}