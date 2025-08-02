using Microsoft.EntityFrameworkCore;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
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
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Missing tenant header");
            return;
        }

        var tenant = await dbContext.Organizations.FirstOrDefaultAsync(o => o.Name == tenantName);
        if (tenant == null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid tenant");
            return;
        }

        context.Items["Tenant"] = tenant;
        await _next(context);
    }
}
