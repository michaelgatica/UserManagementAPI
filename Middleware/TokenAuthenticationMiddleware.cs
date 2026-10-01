using System.Text.Json;

namespace UserManagementAPI.Middleware;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TokenAuthenticationMiddleware> _logger;

    public TokenAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<TokenAuthenticationMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        string expectedToken = _configuration["Authentication:ApiToken"] ?? "techhive-demo-token";
        string? providedToken = context.Request.Headers["X-API-Key"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(providedToken))
        {
            string? authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                providedToken = authorizationHeader["Bearer ".Length..].Trim();
            }
        }

        if (!string.Equals(providedToken, expectedToken, StringComparison.Ordinal))
        {
            _logger.LogWarning("Unauthorized request to {Path} from {RemoteIpAddress}", context.Request.Path, context.Connection.RemoteIpAddress);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var response = new { error = "Unauthorized", message = "A valid API token is required. Use the X-API-Key header or a Bearer token." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }
        await _next(context);
    }
}
