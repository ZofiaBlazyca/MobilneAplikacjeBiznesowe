namespace WorkoutPlanner.API.Models;

public class WorkoutPlanExercise
{
    public int IdWorkoutPlanExercise { get; set; }

    public int IdWorkoutPlan { get; set; }

    public WorkoutPlan WorkoutPlan { get; set; } = null!;

    public int IdExercise { get; set; }

    public Exercise Exercise { get; set; } = null!;

    public int Sets { get; set; }

    public int Reps { get; set; }

    public int? DurationSeconds { get; set; }

    public int OrderNumber { get; set; }
}