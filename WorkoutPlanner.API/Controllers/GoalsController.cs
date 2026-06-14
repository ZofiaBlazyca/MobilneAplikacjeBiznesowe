using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.Goals.Commands.CreateGoal;
using WorkoutPlanner.API.Features.Goals.Commands.DeleteGoal;
using WorkoutPlanner.API.Features.Goals.Commands.UpdateGoal;
using WorkoutPlanner.API.Features.Goals.Queries.GetAllGoals;
using WorkoutPlanner.API.Features.Goals.Queries.GetGoalById;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllGoalsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetGoalByIdQuery(id));

        if (result == null)
        {
            return NotFound(new { message = $"Goal {id} not found" });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id, message = "Goal created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGoalCommand command)
    {
        if (id != command.IdGoal)
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
            await _mediator.Send(new DeleteGoalCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}