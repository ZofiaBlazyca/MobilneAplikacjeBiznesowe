using MediatR;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Commands.CreateExerciseCategory;

public class CreateExerciseCategoryCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}