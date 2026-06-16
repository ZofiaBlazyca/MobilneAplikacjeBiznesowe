using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.ProgressEntries.Queries.GetAllProgressEntries;

public class GetAllProgressEntriesHandler
    : IRequestHandler<GetAllProgressEntriesQuery, List<ProgressEntryDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllProgressEntriesHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProgressEntryDto>> Handle(
        GetAllProgressEntriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ProgressEntries
            .Include(x => x.WorkoutPlan)
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.EntryDate)
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
            .ToListAsync(cancellationToken);
    }
}