using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.UserProfiles.Queries.GetAllUserProfiles;

public class GetAllUserProfilesHandler
    : IRequestHandler<GetAllUserProfilesQuery, List<UserProfileDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllUserProfilesHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserProfileDto>> Handle(
        GetAllUserProfilesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.UserProfiles
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new UserProfileDto
            {
                IdUserProfile = x.IdUserProfile,
                Name = x.Name,
                Email = x.Email,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}