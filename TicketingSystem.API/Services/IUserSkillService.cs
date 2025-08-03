public interface IUserSkillService
{
    Task<IEnumerable<UserSkillDto>> GetAllAsync();
}