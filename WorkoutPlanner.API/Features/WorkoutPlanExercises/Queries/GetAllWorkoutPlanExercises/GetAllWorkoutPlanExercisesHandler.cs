using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.WorkoutPlanExercises.Queries.GetAllWorkoutPlanExercises;

public class GetAllWorkoutPlanExercisesHandler
    : IRequestHandler<GetAllWorkoutPlanExercisesQuery, List<WorkoutPlanExerciseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllWorkoutPlanExercisesHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutPlanExerciseDto>> Handle(
        GetAllWorkoutPlanExercisesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.WorkoutPlanExercises
            .Include(x => x.WorkoutPlan)
            .Include(x => x.Exercise)
            .OrderBy(x => x.IdWorkoutPlan)
            .ThenBy(x => x.OrderNumber)
            .Select(x => new WorkoutPlanExerciseDto
            {
                IdWorkoutPlanExercise = x.IdWorkoutPlanExercise,
                IdWorkoutPlan = x.IdWorkoutPlan,
                WorkoutPlanName = x.WorkoutPlan.Name,
                IdExercise = x.IdExercise,
                ExerciseName = x.Exercise.Name,
                Sets = x.Sets,
                Reps = x.Reps,
                DurationSeconds = x.DurationSeconds,
                OrderNumber = x.OrderNumber
            })
            .ToListAsync(cancellationToken);
    }
}