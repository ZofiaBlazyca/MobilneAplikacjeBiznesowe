using MediatR;

namespace WorkoutPlanner.API.Features.UserProfiles.Queries.GetAllUserProfiles;

public class GetAllUserProfilesQuery : IRequest<List<UserProfileDto>>
{
}

public class UserProfileDto
{
    public int IdUserProfile { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    public bool IsActive { get; set; }
}