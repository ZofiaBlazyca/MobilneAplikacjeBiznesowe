using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Exercises.Commands.DeleteExercise;

public class DeleteExerciseHandler
    : IRequestHandler<DeleteExerciseCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteExerciseHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteExerciseCommand request,
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

        exercise.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}