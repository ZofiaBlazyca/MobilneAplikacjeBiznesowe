namespace WorkoutPlanner.API.Models;

public class ProgressEntry
{
    public int IdProgressEntry { get; set; }

    public int IdWorkoutPlan { get; set; }

    public WorkoutPlan WorkoutPlan { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public int DurationMinutes { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;
}