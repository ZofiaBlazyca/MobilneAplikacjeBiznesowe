using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.WorkoutPlanExercises.Commands.DeleteWorkoutPlanExercise;

public class DeleteWorkoutPlanExerciseHandler
    : IRequestHandler<DeleteWorkoutPlanExerciseCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteWorkoutPlanExerciseHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteWorkoutPlanExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var workoutPlanExercise = await _context.WorkoutPlanExercises
            .FirstOrDefaultAsync(
                x => x.IdWorkoutPlanExercise == request.IdWorkoutPlanExercise,
                cancellationToken);

        if (workoutPlanExercise == null)
        {
            throw new KeyNotFoundException(
                $"Workout plan exercise {request.IdWorkoutPlanExercise} not found");
        }

        _context.WorkoutPlanExercises.Remove(workoutPlanExercise);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}