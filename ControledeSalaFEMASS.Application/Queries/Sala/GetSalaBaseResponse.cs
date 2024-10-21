namespace ControledeSalaFEMASS.Application.Queries.Sala;
public abstract class GetSalaBaseResponse
{
    public int Id { get; set; }
    public string Bloco { get; set; } = string.Empty;
    public long Numero { get; set; }
    public int CapacidadeMaxima { get; set; }
    public bool PossuiLaboratorio { get; set; } = false;
    public bool PossuiArCondicionado { get; set; } = false;
    public bool PossuiLoucaDigital { get; set; } = false;
}