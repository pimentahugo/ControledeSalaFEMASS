using System.ComponentModel.DataAnnotations.Schema;

namespace ControledeSalaFEMASS.Domain.Entities;
public class Turma
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
	public int Id { get; set; }
    public string Professor { get; set; } = string.Empty;
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; } = default!;
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
    public List<Turma>? TurmasGradeAntiga { get; set; }
	public int? TurmaId { get; set; }
	public int? TotalQuantidadeAlunosAgrupados { get; set; }
    public List<AlocacaoSala>? Alocacoes { get; set; }
}