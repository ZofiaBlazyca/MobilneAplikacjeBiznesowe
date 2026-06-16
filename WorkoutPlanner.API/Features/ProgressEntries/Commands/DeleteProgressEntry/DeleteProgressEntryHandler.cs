using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.ProgressEntries.Commands.DeleteProgressEntry;

public class DeleteProgressEntryHandler
    : IRequestHandler<DeleteProgressEntryCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteProgressEntryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteProgressEntryCommand request,
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

        progressEntry.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}