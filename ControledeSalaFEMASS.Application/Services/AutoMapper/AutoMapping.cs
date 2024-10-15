using AutoMapper;
using ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
using ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
using ControledeSalaFEMASS.Application.Commands.Sala.Criar;
using ControledeSalaFEMASS.Domain.Dtos;
using ControledeSalaFEMASS.Domain.Entities;

namespace ControledeSalaFEMASS.Application.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToRequest();
    }

    private void RequestToDomain()
    {
        CreateMap<CriarSalaCommand, Sala>();
        CreateMap<AtualizarSalaCommand, Sala>();
        CreateMap<CriarIndisponibilidadeCommand, Indisponibilidade>();
    }

    private void DomainToRequest()
    {
        CreateMap<Sala, SalaDto>();
        CreateMap<Indisponibilidade, IndisponibilidadeDto>();
        CreateMap<Turma, TurmaDto>()
            .ForMember(p => p.NomeDisciplina, config => config.MapFrom(source => source.Disciplina.Nome));
        CreateMap<AlocacaoSala, AlocacaoDto>();
    }
}