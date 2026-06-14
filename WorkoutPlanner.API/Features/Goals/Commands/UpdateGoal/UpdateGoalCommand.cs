using MediatR;

namespace WorkoutPlanner.API.Features.Goals.Commands.UpdateGoal;

public class UpdateGoalCommand : IRequest<Unit>
{
    public int IdGoal { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? TargetDate { get; set; }

    public bool IsActive { get; set; }
}