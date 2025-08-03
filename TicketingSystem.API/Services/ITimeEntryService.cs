public interface ITimeEntryService
{
    Task<IEnumerable<TimeEntryDto>> GetAllAsync();
}