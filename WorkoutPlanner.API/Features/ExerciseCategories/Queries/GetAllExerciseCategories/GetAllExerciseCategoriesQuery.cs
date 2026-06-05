using MediatR;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetAllExerciseCategories;

public class GetAllExerciseCategoriesQuery : IRequest<List<ExerciseCategoryDto>>
{
}

public class ExerciseCategoryDto
{
    public int IdExerciseCategory { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}