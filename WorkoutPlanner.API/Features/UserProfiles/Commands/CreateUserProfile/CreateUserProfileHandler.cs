using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.UserProfiles.Commands.CreateUserProfile;

public class CreateUserProfileHandler
    : IRequestHandler<CreateUserProfileCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateUserProfileHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        var userProfile = new UserProfile
        {
            Name = request.Name,
            Email = request.Email,
            IsActive = true
        };

        _context.UserProfiles.Add(userProfile);
        await _context.SaveChangesAsync(cancellationToken);

        return userProfile.IdUserProfile;
    }
}