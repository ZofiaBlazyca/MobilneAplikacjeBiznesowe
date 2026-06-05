using MediatR;
using WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetAllExerciseCategories;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetExerciseCategoryById;

public class GetExerciseCategoryByIdQuery : IRequest<ExerciseCategoryDto?>
{
    public int Id { get; set; }

    public GetExerciseCategoryByIdQuery(int id)
    {
        Id = id;
    }
}