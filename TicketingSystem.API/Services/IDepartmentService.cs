public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
}