using System.ComponentModel.DataAnnotations;

/// <summary>
    /// Represents the login information required for user authentication.
    /// Properties:
    /// - Username: Gets or sets the username. This field is required.
    /// - Password: Gets or sets the password. This field is required.
/// </summary>

public class Login
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
