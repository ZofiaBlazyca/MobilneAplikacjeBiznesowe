using MediatR;

namespace WorkoutPlanner.API.Features.Equipment.Commands.UpdateEquipment;

public class UpdateEquipmentCommand : IRequest<Unit>
{
    public int IdEquipment { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}