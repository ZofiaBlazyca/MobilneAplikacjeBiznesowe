using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.Equipment.Commands.CreateEquipment;

public class CreateEquipmentHandler
    : IRequestHandler<CreateEquipmentCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateEquipmentHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateEquipmentCommand request,
        CancellationToken cancellationToken)
    {
        var equipment = new Models.Equipment
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };

        _context.Equipment.Add(equipment);

        await _context.SaveChangesAsync(cancellationToken);

        return equipment.IdEquipment;
    }
}