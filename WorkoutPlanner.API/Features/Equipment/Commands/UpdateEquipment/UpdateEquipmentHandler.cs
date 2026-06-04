using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Equipment.Commands.UpdateEquipment;

public class UpdateEquipmentHandler
    : IRequestHandler<UpdateEquipmentCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateEquipmentHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateEquipmentCommand request,
        CancellationToken cancellationToken)
    {
        var equipment = await _context.Equipment
            .FirstOrDefaultAsync(
                x => x.IdEquipment == request.IdEquipment,
                cancellationToken);

        if (equipment == null)
        {
            throw new KeyNotFoundException(
                $"Equipment {request.IdEquipment} not found");
        }

        equipment.Name = request.Name;
        equipment.Description = request.Description;
        equipment.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}