using MediatR;

namespace WorkoutPlanner.API.Features.Exercises.Commands.CreateExercise;

public class CreateExerciseCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdExerciseCategory { get; set; }

    public int? IdEquipment { get; set; }
}