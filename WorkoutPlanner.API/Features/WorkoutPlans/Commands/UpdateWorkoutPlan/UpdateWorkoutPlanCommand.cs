using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Commands.UpdateWorkoutPlan;

public class UpdateWorkoutPlanCommand : IRequest<Unit>
{
    public int IdWorkoutPlan { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdUserProfile { get; set; }

    public bool IsActive { get; set; }
}