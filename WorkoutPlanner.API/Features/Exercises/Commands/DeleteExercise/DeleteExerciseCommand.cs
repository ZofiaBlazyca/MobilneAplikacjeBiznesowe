using MediatR;

namespace WorkoutPlanner.API.Features.Exercises.Commands.DeleteExercise;

public class DeleteExerciseCommand : IRequest<Unit>
{
    public int IdExercise { get; set; }

    public DeleteExerciseCommand(int idExercise)
    {
        IdExercise = idExercise;
    }
}