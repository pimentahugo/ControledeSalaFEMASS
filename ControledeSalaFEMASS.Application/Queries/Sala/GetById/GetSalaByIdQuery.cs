using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
public class GetSalaByIdQuery : IRequest<GetSalaByIdResponse>
{
    public int Id { get; set; }
}