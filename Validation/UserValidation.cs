using System.ComponentModel.DataAnnotations;
using UserManagementAPI.DTOs;

namespace UserManagementAPI.Validation;

public static class UserValidation
{
    public static Dictionary<string, string[]> ValidateCreate(CreateUserRequest? request)
    {
        var errors = new Dictionary<string, string[]>();
        if (request is null)
        {
            errors["Request"] = new[] { "Request body is required." };
            return errors;
        }
        AddCommonErrors(errors, request.FirstName, request.LastName, request.Email, request.Department, request.Role);
        return errors;
    }

    public static Dictionary<string, string[]> ValidateUpdate(UpdateUserRequest? request)
    {
        var errors = new Dictionary<string, string[]>();
        if (request is null)
        {
            errors["Request"] = new[] { "Request body is required." };
            return errors;
        }
        AddCommonErrors(errors, request.FirstName, request.LastName, request.Email, request.Department, request.Role);
        return errors;
    }

    private static void AddCommonErrors(Dictionary<string, string[]> errors, string? firstName, string? lastName, string? email, string? department, string? role)
    {
        if (string.IsNullOrWhiteSpace(firstName)) errors["FirstName"] = new[] { "First name is required." };
        else if (firstName.Trim().Length > 50) errors["FirstName"] = new[] { "First name cannot exceed 50 characters." };

        if (string.IsNullOrWhiteSpace(lastName)) errors["LastName"] = new[] { "Last name is required." };
        else if (lastName.Trim().Length > 50) errors["LastName"] = new[] { "Last name cannot exceed 50 characters." };

        if (string.IsNullOrWhiteSpace(email)) errors["Email"] = new[] { "Email is required." };
        else if (!new EmailAddressAttribute().IsValid(email.Trim())) errors["Email"] = new[] { "Email must be a valid email address." };

        if (string.IsNullOrWhiteSpace(department)) errors["Department"] = new[] { "Department is required." };
        else if (department.Trim().Length > 75) errors["Department"] = new[] { "Department cannot exceed 75 characters." };

        if (string.IsNullOrWhiteSpace(role)) errors["Role"] = new[] { "Role is required." };
        else if (role.Trim().Length > 75) errors["Role"] = new[] { "Role cannot exceed 75 characters." };
    }
}
