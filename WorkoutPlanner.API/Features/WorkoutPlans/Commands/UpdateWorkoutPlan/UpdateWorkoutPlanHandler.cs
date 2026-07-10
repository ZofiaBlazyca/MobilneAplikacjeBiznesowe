using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Commands.UpdateWorkoutPlan;

public class UpdateWorkoutPlanHandler
    : IRequestHandler<UpdateWorkoutPlanCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateWorkoutPlanHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateWorkoutPlanCommand request,
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

        workoutPlan.Name = request.Name;
        workoutPlan.Description = request.Description;
        workoutPlan.IdUserProfile = request.IdUserProfile;
        workoutPlan.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}