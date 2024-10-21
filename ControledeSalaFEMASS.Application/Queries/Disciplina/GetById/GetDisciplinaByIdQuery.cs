using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
public record GetDisciplinaByIdQuery(long DisciplinaId) : IRequest<GetDisciplinaByIdResponse> { }