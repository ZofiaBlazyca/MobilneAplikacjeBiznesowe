using MediatR;
using Microsoft.AspNetCore.Mvc;

using WorkoutPlanner.API.Features.Equipment.Commands.CreateEquipment;
using WorkoutPlanner.API.Features.Equipment.Commands.DeleteEquipment;
using WorkoutPlanner.API.Features.Equipment.Commands.UpdateEquipment;

using WorkoutPlanner.API.Features.Equipment.Queries.GetAllEquipment;
using WorkoutPlanner.API.Features.Equipment.Queries.GetEquipmentById;

namespace WorkoutPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public EquipmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllEquipmentQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetEquipmentByIdQuery(id));

        if (result == null)
        {
            return NotFound(new
            {
                message = $"Equipment {id} not found"
            });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEquipmentCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new
            {
                id,
                message = "Equipment created"
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateEquipmentCommand command)
    {
        if (id != command.IdEquipment)
        {
            return BadRequest(new
            {
                message = "Id mismatch"
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
                new DeleteEquipmentCommand(id));

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