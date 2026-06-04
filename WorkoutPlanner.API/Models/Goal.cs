namespace WorkoutPlanner.API.Models;

public class Goal
{
    public int IdGoal { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? TargetDate { get; set; }

    public bool IsActive { get; set; } = true;
}