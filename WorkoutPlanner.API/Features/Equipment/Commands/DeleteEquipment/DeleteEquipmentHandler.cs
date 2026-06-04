using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Equipment.Commands.DeleteEquipment;

public class DeleteEquipmentHandler
    : IRequestHandler<DeleteEquipmentCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteEquipmentHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteEquipmentCommand request,
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

        equipment.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}