using MediatR;

namespace WorkoutPlanner.API.Features.ProgressEntries.Commands.UpdateProgressEntry;

public class UpdateProgressEntryCommand : IRequest<Unit>
{
    public int IdProgressEntry { get; set; }

    public int IdWorkoutPlan { get; set; }

    public DateTime EntryDate { get; set; }

    public int DurationMinutes { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }
}