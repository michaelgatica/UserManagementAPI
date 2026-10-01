namespace UserManagementAPI.DTOs;

public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; } = true;
}
