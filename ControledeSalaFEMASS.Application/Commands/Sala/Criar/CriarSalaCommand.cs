using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Criar;
public class CriarSalaCommand : IRequest<SalaDto>
{
    public string Bloco { get; set; }
    public long Numero { get; set; }
    public int CapacidadeMaxima { get; set; }
    public bool PossuiLaboratorio { get; set; }
    public bool PossuiArCondicionado { get; set; }
    public bool PossuiLoucaDigital { get; set; }
}