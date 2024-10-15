using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
public class AtualizarSalaCommand : IRequest
{
    public int Id { get; set; }
    public string Bloco { get; set; }
    public long Numero { get; set; }
    public int CapacidadeMaxima { get; set; }
    public bool PossuiLaboratorio { get; set; }
    public bool PossuiArCondicionado { get; set; }
    public bool PossuiLoucaDigital { get; set; }
}