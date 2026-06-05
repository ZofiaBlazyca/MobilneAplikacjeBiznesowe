using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetAllExerciseCategories;

public class GetAllExerciseCategoriesHandler
    : IRequestHandler<GetAllExerciseCategoriesQuery, List<ExerciseCategoryDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllExerciseCategoriesHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExerciseCategoryDto>> Handle(
        GetAllExerciseCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ExerciseCategories
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new ExerciseCategoryDto
            {
                IdExerciseCategory = x.IdExerciseCategory,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}