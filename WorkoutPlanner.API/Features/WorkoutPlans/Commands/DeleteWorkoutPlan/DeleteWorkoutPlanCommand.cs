using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Commands.DeleteWorkoutPlan;

public class DeleteWorkoutPlanCommand : IRequest<Unit>
{
    public int IdWorkoutPlan { get; set; }

    public DeleteWorkoutPlanCommand(int idWorkoutPlan)
    {
        IdWorkoutPlan = idWorkoutPlan;
    }
}