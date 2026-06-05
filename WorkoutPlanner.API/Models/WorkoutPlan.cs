namespace WorkoutPlanner.API.Models;

public class WorkoutPlan
{
    public int IdWorkoutPlan { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdUserProfile { get; set; }

    public UserProfile UserProfile { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();

    public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = new List<WorkoutPlanExercise>();
}