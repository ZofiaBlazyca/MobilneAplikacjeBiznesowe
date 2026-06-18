using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.ProgressEntries.Queries.GetAllProgressEntries;

namespace WorkoutPlanner.API.Features.ProgressEntries.Queries.GetProgressEntryById;

public class GetProgressEntryByIdHandler
    : IRequestHandler<GetProgressEntryByIdQuery, ProgressEntryDto?>
{
    private readonly ApplicationDbContext _context;

    public GetProgressEntryByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProgressEntryDto?> Handle(
        GetProgressEntryByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ProgressEntries
            .Include(x => x.WorkoutPlan)
            .Where(x => x.IdProgressEntry == request.Id)
            .Select(x => new ProgressEntryDto
            {
                IdProgressEntry = x.IdProgressEntry,
                IdWorkoutPlan = x.IdWorkoutPlan,
                WorkoutPlanName = x.WorkoutPlan.Name,
                EntryDate = x.EntryDate,
                DurationMinutes = x.DurationMinutes,
                Notes = x.Notes,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}