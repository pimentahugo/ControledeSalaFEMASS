using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;

namespace ControledeSalaFEMASS.Application.Commands.Importacao.Turma;
public class ImportarTurmasCommand : IRequest
{
    public List<TurmaImportadaDto> Turmas { get; set; } = [];
}