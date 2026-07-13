using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlanExercises.Queries.GetAllWorkoutPlanExercises;

public class GetAllWorkoutPlanExercisesQuery : IRequest<List<WorkoutPlanExerciseDto>>
{
}

public class WorkoutPlanExerciseDto
{
    public int IdWorkoutPlanExercise { get; set; }
    public int IdWorkoutPlan { get; set; }
    public string WorkoutPlanName { get; set; } = string.Empty;
    public int IdExercise { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int? DurationSeconds { get; set; }
    public int OrderNumber { get; set; }
}