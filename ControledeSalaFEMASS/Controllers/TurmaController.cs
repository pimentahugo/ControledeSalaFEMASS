using ControledeSalaFEMASS.Application.Commands.Importacao.Turma;
using ControledeSalaFEMASS.Application.Commands.Turmas.AlocarSala;
using ControledeSalaFEMASS.Application.Commands.Turmas.DeletarAlocacao;
using ControledeSalaFEMASS.Application.Queries.Turma.GetAll;
using ControledeSalaFEMASS.Application.Queries.Turma.GetTurmaById;
using ControledeSalaFEMASS.Application.Queries.Turma.ObterSalasDisponiveis;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        var query = new GetAllTurmasQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{turmaId}")]
    public async Task<IActionResult> ObterTurmaPorId(int turmaId)
    {
        var query = new GetTurmaByIdQuery() { TurmaId = turmaId};
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("obter-salas-disponiveis/")]
    public async Task<IActionResult> ObterSalasDisponiveis(
        [FromQuery] ObterSalasDisponiveisQuery queryParams)
    {
        var query = queryParams;

        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("alocar-turma")]
    public async Task<IActionResult> AlocarTurmaSala(
        [FromBody] AlocarTurmaSalaCommand request)
    {
        var response = await _mediator.Send(request);

        return Created("", response);
    }

    [HttpDelete("deletar-alocacao/{alocacaoId}")]
    public async Task<IActionResult> DeletarAlocacaoTurma(int alocacaoId)
    {
        var request = new DeletarAlocacaoTurmaCommand() { AlocacaoId = alocacaoId };
        await _mediator.Send(request);
        return Accepted();
    }

    [HttpPost("importar-excel-turmas")]
    public async Task<IActionResult> ImportarExcel(IFormFile file)
    {
        var command = new ImportarTurmasCommand { FileExcel = file };
        await _mediator.Send(command);

        return Accepted();
    }

    [HttpPost("limpar-semestre")]
    public async Task<ActionResult> LimparSemestre()
    {
        var command = new LimparSemestreCommand();

        await _mediator.Send(command);

        return Accepted();
    }
}