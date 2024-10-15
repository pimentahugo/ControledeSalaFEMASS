using ControledeSalaFEMASS.Application.Commands.Importacao.Turma;
using ControledeSalaFEMASS.Application.Queries.Turma.ObterSalasDisponiveis;
using ControledeSalaFEMASS.Application.Queries.Turma.ObterTurmas;
using ControledeSalaFEMASS.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ControledeSalaFEMASS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TurmaController : ControllerBase
{
    private readonly IMediator _mediator;

    public TurmaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTurmas()
    {
        var query = new ObterTurmasQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("obter-salas-disponiveis/{turmaId}")]
    public async Task<IActionResult> ObterSalasDisponiveis(int turmaId)
    {
        var query = new ObterSalasDisponiveisQuery() { TurmaId = turmaId };

        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("importar-excel-turmas")]
    public async Task<IActionResult> ImportarExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo não fornecido ou vazio");

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];

        var turmas = new List<TurmaImportadaDto>();

        for (int row = 2; row <= worksheet.Dimension.End.Row; row++) 
        {
            turmas.Add(new TurmaImportadaDto
            {
                CodigoTurma = worksheet.Cells[row, 1].Text,
                Professor = worksheet.Cells[row, 2].Text,
                Disciplina = worksheet.Cells[row, 3].Text,
                QuantidadeAlunos = int.TryParse(worksheet.Cells[row, 4].Text, out int temp) ? temp : 0,
                CodigoHorario = int.TryParse(worksheet.Cells[row, 5].Text, out int temp2) ? temp2 : 0,
            });
        }

        var command = new ImportarTurmasCommand { Turmas = turmas };
        await _mediator.Send(command);

        return Accepted();
    }

}