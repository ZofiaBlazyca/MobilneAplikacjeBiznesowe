using MediatR;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Commands.DeleteExerciseCategory;

public class DeleteExerciseCategoryCommand : IRequest<Unit>
{
    public int IdExerciseCategory { get; set; }

    public DeleteExerciseCategoryCommand(int idExerciseCategory)
    {
        IdExerciseCategory = idExerciseCategory;
    }
}