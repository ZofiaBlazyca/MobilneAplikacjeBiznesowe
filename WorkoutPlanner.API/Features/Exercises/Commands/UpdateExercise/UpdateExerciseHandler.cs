using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Exercises.Commands.UpdateExercise;

public class UpdateExerciseHandler
    : IRequestHandler<UpdateExerciseCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateExerciseHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(
                x => x.IdExercise == request.IdExercise,
                cancellationToken);

        if (exercise == null)
        {
            throw new KeyNotFoundException(
                $"Exercise {request.IdExercise} not found");
        }

        exercise.Name = request.Name;
        exercise.Description = request.Description;
        exercise.IdExerciseCategory = request.IdExerciseCategory;
        exercise.IdEquipment = request.IdEquipment;
        exercise.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}