using System.ComponentModel.DataAnnotations;

namespace IrasPPBackend.Schemas;

public class LoginPayloadDto
{
    [Required]
    public string JwtToken { get; set; }
}
