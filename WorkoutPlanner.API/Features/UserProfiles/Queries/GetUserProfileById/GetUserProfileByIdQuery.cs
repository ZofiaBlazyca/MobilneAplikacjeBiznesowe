using MediatR;
using WorkoutPlanner.API.Features.UserProfiles.Queries.GetAllUserProfiles;

namespace WorkoutPlanner.API.Features.UserProfiles.Queries.GetUserProfileById;

public class GetUserProfileByIdQuery : IRequest<UserProfileDto?>
{
    public int Id { get; set; }

    public GetUserProfileByIdQuery(int id)
    {
        Id = id;
    }
}