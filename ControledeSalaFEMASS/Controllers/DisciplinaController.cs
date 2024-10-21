using ControledeSalaFEMASS.Application.Commands.Disciplina.Atualizar;
using ControledeSalaFEMASS.Application.Queries.Disciplina.GetAll;
using ControledeSalaFEMASS.Application.Queries.Disciplina.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControledeSalaFEMASS.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DisciplinaController : ControllerBase
{
    private readonly IMediator _mediator;

    public DisciplinaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllDisciplinaQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{disciplinaId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long disciplinaId)
    {
        var query = new GetDisciplinaByIdQuery(disciplinaId);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("{disciplinaId}")]
    public async Task<IActionResult> Update(
        [FromRoute] long disciplinaId,
        [FromBody] AtualizarDisciplinaCommand request)
    {
        request.Id = disciplinaId;

        await _mediator.Send(request);

        return Accepted();
    }
}