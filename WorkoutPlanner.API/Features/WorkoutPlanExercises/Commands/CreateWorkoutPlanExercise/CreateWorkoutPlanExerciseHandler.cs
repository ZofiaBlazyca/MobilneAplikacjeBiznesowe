using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.WorkoutPlanExercises.Commands.CreateWorkoutPlanExercise;

public class CreateWorkoutPlanExerciseHandler
    : IRequestHandler<CreateWorkoutPlanExerciseCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateWorkoutPlanExerciseHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateWorkoutPlanExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var workoutPlanExercise = new WorkoutPlanExercise
        {
            IdWorkoutPlan = request.IdWorkoutPlan,
            IdExercise = request.IdExercise,
            Sets = request.Sets,
            Reps = request.Reps,
            DurationSeconds = request.DurationSeconds,
            OrderNumber = request.OrderNumber
        };

        _context.WorkoutPlanExercises.Add(workoutPlanExercise);

        await _context.SaveChangesAsync(cancellationToken);

        return workoutPlanExercise.IdWorkoutPlanExercise;
    }
}