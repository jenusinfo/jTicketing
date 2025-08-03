public interface ITeamAssignmentService
{
    Task<IEnumerable<TeamAssignmentDto>> GetAllAsync();
}