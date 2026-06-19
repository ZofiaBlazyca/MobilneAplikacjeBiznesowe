using MediatR;

namespace WorkoutPlanner.API.Features.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileCommand : IRequest<Unit>
{
    public int IdUserProfile { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    public bool IsActive { get; set; }
}