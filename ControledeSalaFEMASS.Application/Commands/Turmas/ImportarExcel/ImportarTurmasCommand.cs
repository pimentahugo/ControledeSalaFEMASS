using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ControledeSalaFEMASS.Application.Commands.Importacao.Turma;
public class ImportarTurmasCommand : IRequest
{
    public IFormFile FileExcel;
}