using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
public class ObterSalaPorIdQuery : IRequest<SalaDto>
{
    public int Id { get; set; }
}