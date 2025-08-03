using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantResolutionMiddleware> _logger;

    public TenantResolutionMiddleware(RequestDelegate next, ILogger<TenantResolutionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        if (context.Request.Path.StartsWithSegments("/api/health"))
        {
            await _next(context);
            return;
        }

        var tenantName = context.Request.Headers["X-Tenant"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(tenantName))
        {
            _logger.LogWarning("Missing tenant header.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{ \"error\": \"Missing tenant header.\" }");
            return;
        }

        var tenant = await dbContext.Organizations.FirstOrDefaultAsync(o => o.Name == tenantName);
        if (tenant == null)
        {
            _logger.LogWarning("Invalid tenant: {TenantName}.", tenantName);
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{ \"error\": \"Invalid tenant.\" }");
            return;
        }

        context.Items["Tenant"] = tenant;
        await _next(context);
    }
}
