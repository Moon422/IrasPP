using System.ComponentModel.DataAnnotations;

namespace IrasPPBackend.Schemas;

public class Auth
{
    [Required]
    [MaxLength(64)]
    public string Username { get; set; }

    [Required]
    [MaxLength(32)]
    [MinLength(8)]
    public string Password { get; set; }
}
