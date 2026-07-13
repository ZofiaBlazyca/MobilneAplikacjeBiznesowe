using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.WorkoutPlanExercises.Commands.CreateWorkoutPlanExercise;
using WorkoutPlanner.API.Features.WorkoutPlanExercises.Commands.DeleteWorkoutPlanExercise;
using WorkoutPlanner.API.Features.WorkoutPlanExercises.Queries.GetAllWorkoutPlanExercises;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutPlanExercisesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutPlanExercisesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllWorkoutPlanExercisesQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateWorkoutPlanExerciseCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetAll),
            new { id },
            new
            {
                id,
                message = "Exercise assigned to workout plan"
            });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(
                new DeleteWorkoutPlanExerciseCommand(id));

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