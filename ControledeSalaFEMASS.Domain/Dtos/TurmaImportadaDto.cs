namespace ControledeSalaFEMASS.Domain.Dtos;
public class TurmaImportadaDto
{
    public string CodigoTurma { get; set; } = string.Empty;
    public string Professor { get; set; } = string.Empty;
    public string Disciplina { get; set; } = string.Empty;
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
}