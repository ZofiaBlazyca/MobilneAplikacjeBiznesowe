using MediatR;

namespace WorkoutPlanner.API.Features.ProgressEntries.Queries.GetAllProgressEntries;

public class GetAllProgressEntriesQuery : IRequest<List<ProgressEntryDto>>
{
}

public class ProgressEntryDto
{
    public int IdProgressEntry { get; set; }

    public int IdWorkoutPlan { get; set; }

    public string WorkoutPlanName { get; set; } = string.Empty;

    public DateTime EntryDate { get; set; }

    public int DurationMinutes { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }
}