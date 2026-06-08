using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Exercises.Queries.GetAllExercises;

public class GetAllExercisesHandler
    : IRequestHandler<GetAllExercisesQuery, List<ExerciseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllExercisesHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExerciseDto>> Handle(
        GetAllExercisesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Exercises
            .Include(x => x.ExerciseCategory)
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new ExerciseDto
            {
                IdExercise = x.IdExercise,
                Name = x.Name,
                Description = x.Description,
                IdExerciseCategory = x.IdExerciseCategory,
                CategoryName = x.ExerciseCategory.Name,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}