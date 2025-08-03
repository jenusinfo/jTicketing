public class TeamAssignmentService : ITeamAssignmentService
{
    public Task<IEnumerable<TeamAssignmentDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TeamAssignmentDto>>(new List<TeamAssignmentDto>());
    }
}