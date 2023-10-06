using System;
using System.ComponentModel.DataAnnotations;
using IrasPPBackend.Models;
using BC = BCrypt.Net.BCrypt;

namespace IrasPPBackend.Schemas;

public class AuthDto : IModelConvertible<Auth>
{
    [Required]
    [MaxLength(64)]
    public string Username { get; set; }

    [Required]
    [MaxLength(32)]
    [MinLength(8)]
    public string Password { get; set; }

    public Auth CreateModel()
    {
        return new Auth()
        {
            Username = this.Username,
            HashedPassword = BC.HashPassword(this.Password),
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public abstract class RegistrationDataDto<TDto, TModel> : IModelConvertible<(Auth, TModel)> where TDto : IUserModelConvertible<TModel> where TModel : User
{
    [Required]
    public AuthDto Auth { get; set; }

    [Required]
    public TDto User { get; set; }

    public (Auth, TModel) CreateModel()
    {
        return (this.Auth.CreateModel(), this.User.CreateModel());
    }
}

public class AdminRegistrationDataDto : RegistrationDataDto<AdminCreateDto, Admin> { }

public class ViceChancellorRegistrationDataDto : RegistrationDataDto<ViceChancellorCreateDto, ViceChancellor> { }

public class SchoolAdminRegistrationDataDto : RegistrationDataDto<SchoolAdminCreateDto, SchoolAdmin> { }

public class FacultyRegistrationDataDto : RegistrationDataDto<FacultyCreateDto, Faculty> { }

public class StudentRegistrationDataDto : RegistrationDataDto<StudentCreateDto, Student> { }
