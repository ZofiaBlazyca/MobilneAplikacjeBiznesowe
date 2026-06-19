using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.UserProfiles.Queries.GetAllUserProfiles;

namespace WorkoutPlanner.API.Features.UserProfiles.Queries.GetUserProfileById;

public class GetUserProfileByIdHandler
    : IRequestHandler<GetUserProfileByIdQuery, UserProfileDto?>
{
    private readonly ApplicationDbContext _context;

    public GetUserProfileByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDto?> Handle(
        GetUserProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.UserProfiles
            .Where(x => x.IdUserProfile == request.Id)
            .Select(x => new UserProfileDto
            {
                IdUserProfile = x.IdUserProfile,
                Name = x.Name,
                Email = x.Email,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}