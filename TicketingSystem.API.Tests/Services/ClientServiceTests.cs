using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using System;
using System.Threading.Tasks;

public class ClientServiceTests
{
    private static ClientService CreateService(out AppDbContext context)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        context = new AppDbContext(options);
        var logger = new LoggerFactory().CreateLogger<ClientService>();
        return new ClientService(context, logger);
    }

    [Fact]
    public async Task CreateAsync_AddsClient()
    {
        var service = CreateService(out var context);
        var dto = new ClientCreateDto { Name = "New Client" };

        var result = await service.CreateAsync(dto);

        Assert.Equal("New Client", result.Name);
        Assert.Equal(1, await context.Clients.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_RemovesClient()
    {
        var service = CreateService(out var context);
        var client = new Client { Name = "Delete Me" };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var removed = await service.DeleteAsync(client.Id);

        Assert.True(removed);
        Assert.Empty(context.Clients);
    }
}
