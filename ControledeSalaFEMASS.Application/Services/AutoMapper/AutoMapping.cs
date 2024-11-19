using AutoMapper;
using ControledeSalaFEMASS.Application.Commands.Disciplina.Atualizar;
using ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
using ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
using ControledeSalaFEMASS.Application.Commands.Sala.Criar;
using ControledeSalaFEMASS.Application.Queries.Disciplina;
using ControledeSalaFEMASS.Application.Queries.Disciplina.GetAll;
using ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
using ControledeSalaFEMASS.Application.Queries.Sala.GetAll;
using ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
using ControledeSalaFEMASS.Application.Queries.Turma;
using ControledeSalaFEMASS.Application.Queries.Turma.GetAll;
using ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
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

        CreateMap<AtualizarDisciplinaCommand, Disciplina>();
    }

    private void DomainToRequest()
    {
        CreateMap<Sala, GetSalaByIdResponse>();
        CreateMap<Sala, GetAllSalasResponse>();

        CreateMap<Indisponibilidade, IndisponibilidadeDto>();

        CreateMap<Turma, TurmaResponseBase>();

        CreateMap<Turma, GetAllTurmasResponse>()
            .IncludeBase<Turma, TurmaResponseBase>();
        CreateMap<Turma, GetTurmaByIdResponse>()
            .IncludeBase<Turma, TurmaResponseBase>();

        CreateMap<AlocacaoSala, AlocacaoDto>();
            //.ForMember(p => p.CodigoTurma, config => config.MapFrom(source => source.Turma.CodigoTurma));

        CreateMap<Disciplina, DisciplinaBaseResponse>();

        CreateMap<Disciplina, GetAllDisciplinaResponse>()
            .IncludeBase<Disciplina, DisciplinaBaseResponse>();

        CreateMap<Disciplina, GetDisciplinaByIdResponse>()
            .IncludeBase<Disciplina, DisciplinaBaseResponse>();
    }
}