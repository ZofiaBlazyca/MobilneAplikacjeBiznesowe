using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.ProgressEntries.Commands.CreateProgressEntry;

public class CreateProgressEntryHandler
    : IRequestHandler<CreateProgressEntryCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateProgressEntryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateProgressEntryCommand request,
        CancellationToken cancellationToken)
    {
        var progressEntry = new ProgressEntry
        {
            IdWorkoutPlan = request.IdWorkoutPlan,
            EntryDate = request.EntryDate,
            DurationMinutes = request.DurationMinutes,
            Notes = request.Notes,
            IsActive = true
        };

        _context.ProgressEntries.Add(progressEntry);
        await _context.SaveChangesAsync(cancellationToken);

        return progressEntry.IdProgressEntry;
    }
}