using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Disciplina.GetAll;
public record class GetAllDisciplinaQuery : IRequest<List<GetAllDisciplinaResponse>> { }