using UserManagementAPI.DTOs;
using UserManagementAPI.Middleware;
using UserManagementAPI.Services;
using UserManagementAPI.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// Middleware order: error handling first, authentication second, logging last.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.MapGet("/", () => Results.Ok(new { application = "TechHive Solutions User Management API", instructions = "Use /api/users with X-API-Key: techhive-demo-token." }));
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }));

app.MapGet("/api/users", (IUserService userService, string? search, bool activeOnly) =>
{
    var users = userService.GetUsers(search, activeOnly);
    return Results.Ok(users);
});

app.MapGet("/api/users/{id:int}", (int id, IUserService userService) =>
{
    var user = userService.GetUserById(id);
    return user is null ? Results.NotFound(new { error = "User not found.", id }) : Results.Ok(user);
});

app.MapPost("/api/users", (CreateUserRequest request, IUserService userService) =>
{
    var errors = UserValidation.ValidateCreate(request);
    if (!errors.Any() && userService.EmailExists(request.Email!)) errors["Email"] = new[] { "A user with this email already exists." };
    if (errors.Any()) return Results.ValidationProblem(errors);
    var createdUser = userService.CreateUser(request);
    return Results.Created($"/api/users/{createdUser.Id}", createdUser);
});

app.MapPut("/api/users/{id:int}", (int id, UpdateUserRequest request, IUserService userService) =>
{
    var errors = UserValidation.ValidateUpdate(request);
    if (!errors.Any() && userService.EmailExists(request.Email!, id)) errors["Email"] = new[] { "Another user already has this email address." };
    if (errors.Any()) return Results.ValidationProblem(errors);
    var updatedUser = userService.UpdateUser(id, request);
    return updatedUser is null ? Results.NotFound(new { error = "User not found.", id }) : Results.Ok(updatedUser);
});

app.MapDelete("/api/users/{id:int}", (int id, IUserService userService) =>
{
    bool deleted = userService.DeleteUser(id);
    return deleted ? Results.NoContent() : Results.NotFound(new { error = "User not found.", id });
});

app.MapGet("/api/testing/throw", () =>
{
    throw new InvalidOperationException("This test endpoint intentionally throws an exception.");
});

app.Run();
