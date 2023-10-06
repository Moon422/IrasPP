using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrasPPBackend.Models;

[Table("Auth_T")]
public class AuthDto
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string Username { get; set; }

    [Required]
    [MaxLength(60)]
    public string HashedPassword { get; set; }

    public User User { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}
