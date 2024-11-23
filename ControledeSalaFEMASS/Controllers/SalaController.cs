using ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
using ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Excluir;
using ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
using ControledeSalaFEMASS.Application.Commands.Sala.Criar;
using ControledeSalaFEMASS.Application.Queries.Sala.GetAll;
using ControledeSalaFEMASS.Application.Queries.Sala.GetSalasDocumentByDay;
using ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControledeSalaFEMASS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ListarSalas()
    {
        var query = new GetAllSalasQuery();
        var salas = await _mediator.Send(query);

        return Ok(salas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterSala(int id)
    {
        var query = new GetSalaByIdQuery { Id = id };

        var sala = await _mediator.Send(query);

        return Ok(sala);
    }

    [HttpPost]
    public async Task<IActionResult> CriarSala([FromBody] CriarSalaCommand command)
    {
        var response = await _mediator.Send(command);

        return Created("", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarSala(
        int id, 
        [FromBody] AtualizarSalaCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{id}/indisponibilidade")]
    public async Task<IActionResult> AdicionarIndisponibilidade(int id, [FromBody] CriarIndisponibilidadeCommand command)
    {
        if (id != command.SalaId)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{salaId}/indisponibilidade/{indisponibilidadeId}")]
    public async Task<IActionResult> ExcluirIndisponibilidade(int salaId, int indisponibilidadeId)
    {
        var command = new ExcluirIndisponibilidadeCommand { SalaId = salaId, IndisponibilidadeId = indisponibilidadeId };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("relatorio/{diaSemana}")]
    public async Task<IActionResult> GerarDocumentoSalas(
        DayOfWeek diaSemana)
    {
        var command = new GetSalasDocumentByDayQuery(diaSemana);

        var document = await _mediator.Send(command);

        return File(document, "application/pdf", $"Relatorio_Salas_{diaSemana}.pdf");

	}
}