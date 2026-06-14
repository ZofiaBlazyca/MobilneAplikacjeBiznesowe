using MediatR;

namespace WorkoutPlanner.API.Features.Goals.Queries.GetAllGoals;

public class GetAllGoalsQuery : IRequest<List<GoalDto>>
{
}

public class GoalDto
{
    public int IdGoal { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? TargetDate { get; set; }

    public bool IsActive { get; set; }
}