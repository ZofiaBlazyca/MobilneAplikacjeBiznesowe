using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetAllWorkoutPlans;
using WorkoutPlanner.API.Features.WorkoutPlans.Commands.CreateWorkoutPlan;
using WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetWorkoutPlanById;
using WorkoutPlanner.API.Features.WorkoutPlans.Commands.UpdateWorkoutPlan;
using WorkoutPlanner.API.Features.WorkoutPlans.Commands.DeleteWorkoutPlan;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutPlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutPlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllWorkoutPlansQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateWorkoutPlanCommand command)
    {
        var workoutPlanId = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = workoutPlanId },
            new
            {
                id = workoutPlanId,
                message = "Workout plan created"
            });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetWorkoutPlanByIdQuery(id));

        if (result == null)
        {
            return NotFound(new
            {
                message = $"Workout plan with ID {id} was not found"
            });
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateWorkoutPlanCommand command)
    {
        if (id != command.IdWorkoutPlan)
        {
            return BadRequest();
        }

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(
                new DeleteWorkoutPlanCommand(id));

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }
}