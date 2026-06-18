using MediatR;

namespace WorkoutPlanner.API.Features.ProgressEntries.Commands.CreateProgressEntry;

public class CreateProgressEntryCommand : IRequest<int>
{
    public int IdWorkoutPlan { get; set; }

    public DateTime EntryDate { get; set; }

    public int DurationMinutes { get; set; }

    public string? Notes { get; set; }
}