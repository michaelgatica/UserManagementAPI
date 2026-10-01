using UserManagementAPI.DTOs;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, FirstName = "Alicia", LastName = "Garcia", Email = "alicia.garcia@techhive.local", Department = "Human Resources", Role = "HR Manager", IsActive = true },
        new User { Id = 2, FirstName = "Marcus", LastName = "Lee", Email = "marcus.lee@techhive.local", Department = "Information Technology", Role = "Systems Analyst", IsActive = true },
        new User { Id = 3, FirstName = "Priya", LastName = "Patel", Email = "priya.patel@techhive.local", Department = "Finance", Role = "Accountant", IsActive = true }
    };

    private int _nextId = 4;

    public IReadOnlyList<User> GetUsers(string? search = null, bool activeOnly = false)
    {
        IEnumerable<User> query = _users;
        if (activeOnly) query = query.Where(user => user.IsActive);
        if (!string.IsNullOrWhiteSpace(search))
        {
            string value = search.Trim();
            query = query.Where(user =>
                user.FirstName.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                user.LastName.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                user.Email.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                user.Department.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                user.Role.Contains(value, StringComparison.OrdinalIgnoreCase));
        }
        return query.OrderBy(user => user.LastName).ThenBy(user => user.FirstName).ToList();
    }

    public User? GetUserById(int id) => _users.FirstOrDefault(user => user.Id == id);

    public User CreateUser(CreateUserRequest request)
    {
        var user = new User
        {
            Id = _nextId++,
            FirstName = request.FirstName!.Trim(),
            LastName = request.LastName!.Trim(),
            Email = request.Email!.Trim().ToLowerInvariant(),
            Department = request.Department!.Trim(),
            Role = request.Role!.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _users.Add(user);
        return user;
    }

    public User? UpdateUser(int id, UpdateUserRequest request)
    {
        User? existingUser = GetUserById(id);
        if (existingUser is null) return null;
        existingUser.FirstName = request.FirstName!.Trim();
        existingUser.LastName = request.LastName!.Trim();
        existingUser.Email = request.Email!.Trim().ToLowerInvariant();
        existingUser.Department = request.Department!.Trim();
        existingUser.Role = request.Role!.Trim();
        existingUser.IsActive = request.IsActive;
        existingUser.UpdatedAt = DateTime.UtcNow;
        return existingUser;
    }

    public bool DeleteUser(int id)
    {
        User? user = GetUserById(id);
        if (user is null) return false;
        _users.Remove(user);
        return true;
    }

    public bool EmailExists(string email, int? ignoreUserId = null)
    {
        return _users.Any(user => user.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase) && (!ignoreUserId.HasValue || user.Id != ignoreUserId.Value));
    }
}
