using ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Criar;
using ControledeSalaFEMASS.Application.Commands.Indisponibilidade.Excluir;
using ControledeSalaFEMASS.Application.Commands.Sala.Atualizar;
using ControledeSalaFEMASS.Application.Commands.Sala.Criar;
using ControledeSalaFEMASS.Application.Queries.Sala.ObterPorId;
using ControledeSalaFEMASS.Application.Queries.Sala.ObterTodas;
using ControledeSalaFEMASS.Domain.Dtos;
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
    public async Task<ActionResult<List<SalaDto>>> ListarSalas()
    {
        var query = new ObterTodasSalasQuery();
        var salas = await _mediator.Send(query);
        return salas;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaDto>> ObterSala(int id)
    {
        var query = new ObterSalaPorIdQuery { Id = id };
        var sala = await _mediator.Send(query);

        if (sala is null)
            return NotFound();

        return sala!;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CriarSala([FromBody] CriarSalaCommand command)
    {
        var id = await _mediator.Send(command);
        return Created("", id);
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
}