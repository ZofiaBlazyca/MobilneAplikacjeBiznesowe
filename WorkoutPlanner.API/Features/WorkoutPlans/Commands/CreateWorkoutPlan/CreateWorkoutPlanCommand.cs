using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Commands.CreateWorkoutPlan;

public class CreateWorkoutPlanCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdUserProfile { get; set; }
}