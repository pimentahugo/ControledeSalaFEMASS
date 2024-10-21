using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
public class GetTurmaByIdQuery : IRequest<GetTurmaByIdResponse>
{
    public int TurmaId { get; set; }
}