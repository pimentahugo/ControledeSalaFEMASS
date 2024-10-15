using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Turma.ObterSalasDisponiveis;
public class ObterSalasDisponiveisQuery : IRequest<List<Domain.Entities.Sala>>
{
    public int TurmaId { get; set; }
}