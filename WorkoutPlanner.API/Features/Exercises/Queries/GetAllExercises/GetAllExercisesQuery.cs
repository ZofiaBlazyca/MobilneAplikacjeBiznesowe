using MediatR;

namespace WorkoutPlanner.API.Features.Exercises.Queries.GetAllExercises;

public class GetAllExercisesQuery : IRequest<List<ExerciseDto>>
{
}

public class ExerciseDto
{
    public int IdExercise { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IdExerciseCategory { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}