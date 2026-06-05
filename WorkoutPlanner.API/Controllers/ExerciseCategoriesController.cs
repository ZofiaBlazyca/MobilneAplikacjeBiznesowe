using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.ExerciseCategories.Commands.CreateExerciseCategory;
using WorkoutPlanner.API.Features.ExerciseCategories.Commands.DeleteExerciseCategory;
using WorkoutPlanner.API.Features.ExerciseCategories.Commands.UpdateExerciseCategory;
using WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetAllExerciseCategories;
using WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetExerciseCategoryById;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExerciseCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllExerciseCategoriesQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetExerciseCategoryByIdQuery(id));

        if (result == null)
        {
            return NotFound(new
            {
                message = $"Exercise category with ID {id} was not found"
            });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateExerciseCategoryCommand command)
    {
        var categoryId = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = categoryId },
            new
            {
                id = categoryId,
                message = "Exercise category created"
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateExerciseCategoryCommand command)
    {
        if (id != command.IdExerciseCategory)
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
                new DeleteExerciseCategoryCommand(id));

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