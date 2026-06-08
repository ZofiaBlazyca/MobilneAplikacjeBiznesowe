using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.Exercises.Commands.CreateExercise;

public class CreateExerciseHandler
    : IRequestHandler<CreateExerciseCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateExerciseHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var exercise = new Exercise
        {
            Name = request.Name,
            Description = request.Description,
            IdExerciseCategory = request.IdExerciseCategory,
            IdEquipment = request.IdEquipment,
            IsActive = true
        };

        _context.Exercises.Add(exercise);

        await _context.SaveChangesAsync(cancellationToken);

        return exercise.IdExercise;
    }
}