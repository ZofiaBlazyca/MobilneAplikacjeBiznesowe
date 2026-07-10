using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Commands.DeleteWorkoutPlan;

public class DeleteWorkoutPlanHandler
    : IRequestHandler<DeleteWorkoutPlanCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteWorkoutPlanHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteWorkoutPlanCommand request,
        CancellationToken cancellationToken)
    {
        var workoutPlan = await _context.WorkoutPlans
            .FirstOrDefaultAsync(
                x => x.IdWorkoutPlan == request.IdWorkoutPlan,
                cancellationToken);

        if (workoutPlan == null)
        {
            throw new KeyNotFoundException(
                $"Workout plan {request.IdWorkoutPlan} not found");
        }

        // Soft Delete
        workoutPlan.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}