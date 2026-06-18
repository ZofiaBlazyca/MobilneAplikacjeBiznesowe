using MediatR;

namespace WorkoutPlanner.API.Features.UserProfiles.Commands.CreateUserProfile;

public class CreateUserProfileCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }
}