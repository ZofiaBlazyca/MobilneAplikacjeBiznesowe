using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.ProgressEntries.Commands.UpdateProgressEntry;

public class UpdateProgressEntryHandler
    : IRequestHandler<UpdateProgressEntryCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateProgressEntryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateProgressEntryCommand request,
        CancellationToken cancellationToken)
    {
        var progressEntry = await _context.ProgressEntries
            .FirstOrDefaultAsync(
                x => x.IdProgressEntry == request.IdProgressEntry,
                cancellationToken);

        if (progressEntry == null)
        {
            throw new KeyNotFoundException(
                $"Progress entry {request.IdProgressEntry} not found");
        }

        progressEntry.IdWorkoutPlan = request.IdWorkoutPlan;
        progressEntry.EntryDate = request.EntryDate;
        progressEntry.DurationMinutes = request.DurationMinutes;
        progressEntry.Notes = request.Notes;
        progressEntry.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}