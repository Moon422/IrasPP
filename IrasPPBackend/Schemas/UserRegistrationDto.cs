using System.ComponentModel.DataAnnotations;
using IrasPPBackend.Models;

namespace IrasPPBackend.Schemas;

public abstract class UserRegistrationDto<T> where T : User
{
    [Required]
    public AuthDto Auth { get; set; }

    [Required]
    public T User { get; set; }

    public abstract T CreateModel();
}

public class AdminRegistrationDto : UserRegistrationDto<Admin>
{
    public override Admin CreateModel()
    {
        return new Admin
        {

        }
    }
}
