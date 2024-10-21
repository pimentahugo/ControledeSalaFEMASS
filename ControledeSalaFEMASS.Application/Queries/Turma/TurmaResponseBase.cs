namespace ControledeSalaFEMASS.Application.Queries.Turma;
public abstract class TurmaResponseBase
{
    public string CodigoTurma { get; set; } = string.Empty;
    public string Professor { get; set; } = string.Empty;
    public string NomeDisciplina { get; set; }
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
}