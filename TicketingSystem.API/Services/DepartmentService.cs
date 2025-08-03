public class DepartmentService : IDepartmentService
{
    public Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<DepartmentDto>>(new List<DepartmentDto>());
    }
}