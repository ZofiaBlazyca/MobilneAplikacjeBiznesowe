using MediatR;

namespace WorkoutPlanner.API.Features.Goals.Commands.DeleteGoal;

public class DeleteGoalCommand : IRequest<Unit>
{
    public int IdGoal { get; set; }

    public DeleteGoalCommand(int idGoal)
    {
        IdGoal = idGoal;
    }
}