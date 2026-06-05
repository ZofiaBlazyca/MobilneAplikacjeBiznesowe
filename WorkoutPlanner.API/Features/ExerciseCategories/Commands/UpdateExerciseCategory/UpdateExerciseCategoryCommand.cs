using MediatR;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Commands.UpdateExerciseCategory;

public class UpdateExerciseCategoryCommand : IRequest<Unit>
{
    public int IdExerciseCategory { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}