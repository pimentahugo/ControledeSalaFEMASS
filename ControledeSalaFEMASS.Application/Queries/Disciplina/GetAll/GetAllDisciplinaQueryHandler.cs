using AutoMapper;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Disciplina.GetAll;
public class GetAllDisciplinaQueryHandler : IRequestHandler<GetAllDisciplinaQuery, List<GetAllDisciplinaResponse>>
{
    private readonly IDisciplinaRepository _repository;
    private readonly IMapper _mapper;

    public GetAllDisciplinaQueryHandler(
        IDisciplinaRepository repository, 
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllDisciplinaResponse>> Handle(GetAllDisciplinaQuery request, CancellationToken cancellationToken)
    {
        var disciplinas = await _repository.GetAll();

        return _mapper.Map<List<GetAllDisciplinaResponse>>(disciplinas);
    }
}