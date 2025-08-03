public interface ISlaPolicyService
{
    Task<IEnumerable<SlaPolicyDto>> GetAllAsync();
}