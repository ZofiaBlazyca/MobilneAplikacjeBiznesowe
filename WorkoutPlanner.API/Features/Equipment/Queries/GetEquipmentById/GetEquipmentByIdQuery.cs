using MediatR;
using WorkoutPlanner.API.Features.Equipment.Queries.GetAllEquipment;

namespace WorkoutPlanner.API.Features.Equipment.Queries.GetEquipmentById;

public class GetEquipmentByIdQuery : IRequest<EquipmentDto?>
{
    public int Id { get; set; }

    public GetEquipmentByIdQuery(int id)
    {
        Id = id;
    }
}