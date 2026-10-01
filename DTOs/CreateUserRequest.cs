namespace UserManagementAPI.DTOs;

public class CreateUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }
    public string? Role { get; set; }
}
