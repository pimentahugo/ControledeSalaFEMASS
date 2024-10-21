namespace ControledeSalaFEMASS.Application.Commands.Sala;
public abstract class SalaCommandBase
{
    public int CapacidadeMaxima { get; set; }
    public bool PossuiLaboratorio { get; set; }
    public bool PossuiArCondicionado { get; set; }
    public bool PossuiLoucaDigital { get; set; }
}