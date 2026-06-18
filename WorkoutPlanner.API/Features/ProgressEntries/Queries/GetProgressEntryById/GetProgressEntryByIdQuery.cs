using MediatR;
using WorkoutPlanner.API.Features.ProgressEntries.Queries.GetAllProgressEntries;

namespace WorkoutPlanner.API.Features.ProgressEntries.Queries.GetProgressEntryById;

public class GetProgressEntryByIdQuery : IRequest<ProgressEntryDto?>
{
    public int Id { get; set; }

    public GetProgressEntryByIdQuery(int id)
    {
        Id = id;
    }
}