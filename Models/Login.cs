using System.ComponentModel.DataAnnotations;

public class Login
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
