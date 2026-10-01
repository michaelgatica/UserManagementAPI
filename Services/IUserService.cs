using UserManagementAPI.DTOs;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public interface IUserService
{
    IReadOnlyList<User> GetUsers(string? search = null, bool activeOnly = false);
    User? GetUserById(int id);
    User CreateUser(CreateUserRequest request);
    User? UpdateUser(int id, UpdateUserRequest request);
    bool DeleteUser(int id);
    bool EmailExists(string email, int? ignoreUserId = null);
}
