namespace WorkoutPlanner.API.Models;

public class Equipment
{
    public int IdEquipment { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}