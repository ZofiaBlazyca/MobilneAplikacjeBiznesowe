namespace WorkoutPlanner.API.Models;

public class UserProfile
{
    public int IdUserProfile { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}