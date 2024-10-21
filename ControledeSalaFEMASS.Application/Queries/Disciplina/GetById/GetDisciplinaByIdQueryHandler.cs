using AutoMapper;
using ControledeSalaFEMASS.Domain.Exceptions;
using ControledeSalaFEMASS.Domain.Repositories;
using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
public class GetDisciplinaByIdQueryHandler : IRequestHandler<GetDisciplinaByIdQuery, GetDisciplinaByIdResponse>
{
    private readonly IDisciplinaRepository _repository;
    private readonly IMapper _mapper;

    public GetDisciplinaByIdQueryHandler(IDisciplinaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetDisciplinaByIdResponse> Handle(GetDisciplinaByIdQuery request, CancellationToken cancellationToken)
    {
        var disciplina = await _repository.GetById(request.DisciplinaId);

        if(disciplina is null)
        {
            throw new NotFoundException("A disciplina informada não foi encontrada na base de dados");
        }

        return _mapper.Map<GetDisciplinaByIdResponse>(disciplina);
    }
}
