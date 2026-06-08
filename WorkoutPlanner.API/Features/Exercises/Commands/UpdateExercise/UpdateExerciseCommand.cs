using MediatR;

namespace WorkoutPlanner.API.Features.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommand : IRequest<Unit>
{
    public int IdExercise { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdExerciseCategory { get; set; }

    public int? IdEquipment { get; set; }

    public bool IsActive { get; set; }
}