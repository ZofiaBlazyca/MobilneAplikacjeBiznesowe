using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.Exercises.Queries.GetAllExercises;

namespace WorkoutPlanner.API.Features.Exercises.Queries.GetExerciseById;

public class GetExerciseByIdHandler
    : IRequestHandler<GetExerciseByIdQuery, ExerciseDto?>
{
    private readonly ApplicationDbContext _context;

    public GetExerciseByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExerciseDto?> Handle(
        GetExerciseByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Exercises
            .Include(x => x.ExerciseCategory)
            .Where(x => x.IdExercise == request.Id)
            .Select(x => new ExerciseDto
            {
                IdExercise = x.IdExercise,
                Name = x.Name,
                Description = x.Description,
                IdExerciseCategory = x.IdExerciseCategory,
                CategoryName = x.ExerciseCategory.Name,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}