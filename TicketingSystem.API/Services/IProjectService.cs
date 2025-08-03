public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> GetByIdAsync(int id);
    Task<ProjectDto> CreateAsync(ProjectCreateDto dto);
}