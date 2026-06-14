using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Goals.Commands.DeleteGoal;

public class DeleteGoalHandler
    : IRequestHandler<DeleteGoalCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteGoalHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteGoalCommand request,
        CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(x => x.IdGoal == request.IdGoal, cancellationToken);

        if (goal == null)
        {
            throw new KeyNotFoundException($"Goal {request.IdGoal} not found");
        }

        goal.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}