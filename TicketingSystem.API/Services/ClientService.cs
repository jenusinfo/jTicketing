using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ClientService> _logger;

    public ClientService(AppDbContext context, ILogger<ClientService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        try
        {
            return await _context.Clients
                .Select(c => new ClientDto { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving clients");
            throw;
        }
    }

    public async Task<ClientDto> GetByIdAsync(int id)
    {
        try
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return null;

            return new ClientDto { Id = client.Id, Name = client.Name };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving client by id");
            throw;
        }
    }

    public async Task<ClientDto> CreateAsync(ClientCreateDto dto)
    {
        try
        {
            var client = new Client { Name = dto.Name };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return new ClientDto { Id = client.Id, Name = client.Name };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating client");
            throw;
        }
    }

    public async Task<ClientDto> UpdateAsync(int id, ClientUpdateDto dto)
    {
        try
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return null;

            client.Name = dto.Name;
            await _context.SaveChangesAsync();

            return new ClientDto { Id = client.Id, Name = client.Name };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating client");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting client");
            throw;
        }
    }
}
