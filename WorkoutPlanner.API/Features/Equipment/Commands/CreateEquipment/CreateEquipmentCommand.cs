using MediatR;

namespace WorkoutPlanner.API.Features.Equipment.Commands.CreateEquipment;

public class CreateEquipmentCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}