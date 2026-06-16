using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.ProgressEntries.Commands.CreateProgressEntry;
using WorkoutPlanner.API.Features.ProgressEntries.Commands.DeleteProgressEntry;
using WorkoutPlanner.API.Features.ProgressEntries.Commands.UpdateProgressEntry;
using WorkoutPlanner.API.Features.ProgressEntries.Queries.GetAllProgressEntries;
using WorkoutPlanner.API.Features.ProgressEntries.Queries.GetProgressEntryById;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressEntriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProgressEntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProgressEntriesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProgressEntryByIdQuery(id));

        if (result == null)
        {
            return NotFound(new { message = $"Progress entry {id} not found" });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProgressEntryCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id, message = "Progress entry created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateProgressEntryCommand command)
    {
        if (id != command.IdProgressEntry)
        {
            return BadRequest(new { message = "Id mismatch" });
        }

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(new DeleteProgressEntryCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}