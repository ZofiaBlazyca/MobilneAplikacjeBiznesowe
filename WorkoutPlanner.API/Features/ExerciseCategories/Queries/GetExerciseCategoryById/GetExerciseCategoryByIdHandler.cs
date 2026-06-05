using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetAllExerciseCategories;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Queries.GetExerciseCategoryById;

public class GetExerciseCategoryByIdHandler
    : IRequestHandler<GetExerciseCategoryByIdQuery, ExerciseCategoryDto?>
{
    private readonly ApplicationDbContext _context;

    public GetExerciseCategoryByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExerciseCategoryDto?> Handle(
        GetExerciseCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ExerciseCategories
            .Where(x => x.IdExerciseCategory == request.Id)
            .Select(x => new ExerciseCategoryDto
            {
                IdExerciseCategory = x.IdExerciseCategory,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}