using MediatR;

namespace WorkoutPlanner.API.Features.Equipment.Queries.GetAllEquipment;

public class GetAllEquipmentQuery : IRequest<List<EquipmentDto>>
{
}

public class EquipmentDto
{
    public int IdEquipment { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}