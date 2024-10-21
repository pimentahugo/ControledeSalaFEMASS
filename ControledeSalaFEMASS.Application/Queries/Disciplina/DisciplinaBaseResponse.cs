namespace ControledeSalaFEMASS.Application.Queries.Disciplina;
public abstract class DisciplinaBaseResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool NecessitaLaboratorio { get; set; } = false;
    public bool NecessitaArCondicionado { get; set; } = false;
    public bool NecessitaLoucaDigital { get; set; } = false;
}