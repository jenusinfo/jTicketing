public class UserSkillService : IUserSkillService
{
    public Task<IEnumerable<UserSkillDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<UserSkillDto>>(new List<UserSkillDto>());
    }
}