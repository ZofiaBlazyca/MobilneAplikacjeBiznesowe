using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Commands.DeleteExerciseCategory;

public class DeleteExerciseCategoryHandler
    : IRequestHandler<DeleteExerciseCategoryCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteExerciseCategoryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteExerciseCategoryCommand request,
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

        category.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}