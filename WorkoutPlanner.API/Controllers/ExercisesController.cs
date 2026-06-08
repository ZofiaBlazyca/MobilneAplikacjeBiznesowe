using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.Exercises.Queries.GetAllExercises;
using WorkoutPlanner.API.Features.Exercises.Commands.CreateExercise;
using WorkoutPlanner.API.Features.Exercises.Commands.DeleteExercise;
using WorkoutPlanner.API.Features.Exercises.Commands.UpdateExercise;
using WorkoutPlanner.API.Features.Exercises.Queries.GetExerciseById;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExercisesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllExercisesQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateExerciseCommand command)
    {
        var exerciseId = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = exerciseId },
            new
            {
                id = exerciseId,
                message = "Exercise created"
            });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetExerciseByIdQuery(id));

        if (result == null)
        {
            return NotFound(new
            {
                message = $"Exercise with ID {id} was not found"
            });
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateExerciseCommand command)
    {
        if (id != command.IdExercise)
        {
            return BadRequest(new
            {
                message = "ID from URL is different than ID from body"
            });
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
                new DeleteExerciseCommand(id));

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