using ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Sala.Criar;
public class CriarSalaCommand : SalaCommandBase, IRequest<GetSalaByIdResponse>
{
    public string Bloco { get; set; }
    public long Numero { get; set; }
}