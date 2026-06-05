using MediatR;

namespace WorkoutPlanner.API.Features.Equipment.Commands.DeleteEquipment;

public class DeleteEquipmentCommand : IRequest<Unit>
{
    public int IdEquipment { get; set; }

    public DeleteEquipmentCommand(int idEquipment)
    {
        IdEquipment = idEquipment;
    }
}