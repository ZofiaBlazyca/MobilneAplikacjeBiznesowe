using MediatR;

namespace WorkoutPlanner.API.Features.ProgressEntries.Commands.DeleteProgressEntry;

public class DeleteProgressEntryCommand : IRequest<Unit>
{
    public int IdProgressEntry { get; set; }

    public DeleteProgressEntryCommand(int idProgressEntry)
    {
        IdProgressEntry = idProgressEntry;
    }
}