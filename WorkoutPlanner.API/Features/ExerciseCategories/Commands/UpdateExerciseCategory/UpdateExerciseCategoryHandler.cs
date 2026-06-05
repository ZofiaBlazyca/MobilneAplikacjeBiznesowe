using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Commands.UpdateExerciseCategory;

public class UpdateExerciseCategoryHandler
    : IRequestHandler<UpdateExerciseCategoryCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateExerciseCategoryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateExerciseCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _context.ExerciseCategories
            .FirstOrDefaultAsync(
                x => x.IdExerciseCategory == request.IdExerciseCategory,
                cancellationToken);

        if (category == null)
        {
            throw new KeyNotFoundException(
                $"Exercise category {request.IdExerciseCategory} not found");
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}