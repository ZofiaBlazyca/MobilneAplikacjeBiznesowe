namespace WorkoutPlanner.API.Models;

public class Exercise
{
    public int IdExercise { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdExerciseCategory { get; set; }

    public ExerciseCategory ExerciseCategory { get; set; } = null!;

    public int? IdEquipment { get; set; }

    public Equipment? Equipment { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = new List<WorkoutPlanExercise>();
}