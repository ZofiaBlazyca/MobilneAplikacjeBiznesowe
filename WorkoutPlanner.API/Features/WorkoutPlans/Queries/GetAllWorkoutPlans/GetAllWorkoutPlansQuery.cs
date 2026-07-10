using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetAllWorkoutPlans;

public class GetAllWorkoutPlansQuery : IRequest<List<WorkoutPlanDto>>
{
}

public class WorkoutPlanDto
{
    public int IdWorkoutPlan { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdUserProfile { get; set; }

    public string UserName { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}