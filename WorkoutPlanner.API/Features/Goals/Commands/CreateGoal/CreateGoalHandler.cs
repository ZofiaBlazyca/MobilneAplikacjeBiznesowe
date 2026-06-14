using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.Goals.Commands.CreateGoal;

public class CreateGoalHandler
    : IRequestHandler<CreateGoalCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateGoalHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateGoalCommand request,
        CancellationToken cancellationToken)
    {
        var goal = new Goal
        {
            Name = request.Name,
            Description = request.Description,
            TargetDate = request.TargetDate,
            IsActive = true
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return goal.IdGoal;
    }
}