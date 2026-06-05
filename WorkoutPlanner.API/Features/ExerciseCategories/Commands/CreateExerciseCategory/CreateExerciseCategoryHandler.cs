using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.ExerciseCategories.Commands.CreateExerciseCategory;

public class CreateExerciseCategoryHandler
    : IRequestHandler<CreateExerciseCategoryCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateExerciseCategoryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateExerciseCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new ExerciseCategory
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };

        _context.ExerciseCategories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.IdExerciseCategory;
    }
}