using MediatR;

namespace WorkoutPlanner.API.Features.Goals.Commands.CreateGoal;

public class CreateGoalCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? TargetDate { get; set; }
}