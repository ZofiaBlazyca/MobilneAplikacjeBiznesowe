using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Goals.Commands.UpdateGoal;

public class UpdateGoalHandler
    : IRequestHandler<UpdateGoalCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateGoalHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateGoalCommand request,
        CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(x => x.IdGoal == request.IdGoal, cancellationToken);

        if (goal == null)
        {
            throw new KeyNotFoundException($"Goal {request.IdGoal} not found");
        }

        goal.Name = request.Name;
        goal.Description = request.Description;
        goal.TargetDate = request.TargetDate;
        goal.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}