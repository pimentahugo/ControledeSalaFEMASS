namespace ControledeSalaFEMASS.Domain.Dtos;
public class TurmaImportadaDto
{
    public string CodigoTurma { get; set; }
    public string Professor { get; set; }
    public string Disciplina { get; set; }
    public int? QuantidadeAlunos { get; set; }
    public int? CodigoHorario { get; set; }
}