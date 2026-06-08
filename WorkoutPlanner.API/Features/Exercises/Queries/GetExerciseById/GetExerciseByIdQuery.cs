using MediatR;
using WorkoutPlanner.API.Features.Exercises.Queries.GetAllExercises;

namespace WorkoutPlanner.API.Features.Exercises.Queries.GetExerciseById;

public class GetExerciseByIdQuery : IRequest<ExerciseDto?>
{
    public int Id { get; set; }

    public GetExerciseByIdQuery(int id)
    {
        Id = id;
    }
}