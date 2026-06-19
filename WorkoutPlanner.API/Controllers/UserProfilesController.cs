using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.API.Features.UserProfiles.Commands.CreateUserProfile;
using WorkoutPlanner.API.Features.UserProfiles.Commands.DeleteUserProfile;
using WorkoutPlanner.API.Features.UserProfiles.Commands.UpdateUserProfile;
using WorkoutPlanner.API.Features.UserProfiles.Queries.GetAllUserProfiles;
using WorkoutPlanner.API.Features.UserProfiles.Queries.GetUserProfileById;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserProfilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUserProfilesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserProfileByIdQuery(id));

        if (result == null)
        {
            return NotFound(new { message = $"User profile {id} not found" });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserProfileCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id, message = "User profile created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserProfileCommand command)
    {
        if (id != command.IdUserProfile)
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
            await _mediator.Send(new DeleteUserProfileCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}