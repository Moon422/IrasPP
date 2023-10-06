using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Security.Authentication;
using System.Security.Claims;
using IrasPPBackend.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using IrasPPBackend.Models;

namespace IrasPPBackend.Services;

public interface IAuthService
{
    Task<LoginPayloadDto> Login(AuthDto auth);
    Task<LoginPayloadDto> Register(AdminRegistrationDataDto registrationDataDto);
    Task<LoginPayloadDto> Register(ViceChancellorRegistrationDataDto registrationDataDto);
    Task<LoginPayloadDto> Register(SchoolAdminRegistrationDataDto registrationDataDto);
    Task<LoginPayloadDto> Register(FacultyRegistrationDataDto registrationDataDto);
    Task<LoginPayloadDto> Register(StudentRegistrationDataDto registrationDataDto);
}

public class AuthService : IAuthService
{
    private readonly IrasDbContext dbContext;
    private readonly IConfiguration configuration;

    public AuthService(IrasDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
    }

    private string EncodeJwt(string username, string email, string phonenumber)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.MobilePhone, phonenumber)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.configuration["Secret"]!)
        );

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private LoginPayloadDto CreateLoginPayload(string username, string email, string phonenumber)
    {
        var jwt = EncodeJwt(username, email, phonenumber);

        return new LoginPayloadDto()
        {
            JwtToken = jwt
        };
    }

    public async Task<LoginPayloadDto> Login(AuthDto auth)
    {
        if (auth == null)
        {
            throw new ArgumentNullException("auth", "auth object cannot be null");
        }

        try
        {
            var authObj = await this.dbContext.Auths.Include(a => a.User).FirstAsync(a => a.Username == auth.Username);
            var userObj = authObj.User;

            if (BC.Verify(auth.Password, authObj.HashedPassword))
            {
                return CreateLoginPayload(authObj.Username, userObj.Email, userObj.PhoneNumber);
            }
            else
            {
                throw new InvalidCredentialException("Invalid credentials");
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidCredentialException("Invalid credentials", ex);
        }
    }

    public async Task<LoginPayloadDto> Register(AdminRegistrationDataDto registrationDataDto)
    {
        (Auth auth, Admin user) = registrationDataDto.CreateModel();
        user.Auth = auth;

        await dbContext.Auths.AddAsync(auth);
        await dbContext.Admins.AddAsync(user);
        await dbContext.SaveChangesAsync(true);

        return CreateLoginPayload(auth.Username, user.Email, user.PhoneNumber);
    }

    public async Task<LoginPayloadDto> Register(ViceChancellorRegistrationDataDto registrationDataDto)
    {
        (Auth auth, ViceChancellor user) = registrationDataDto.CreateModel();
        user.Auth = auth;

        await dbContext.Auths.AddAsync(auth);
        await dbContext.ViceChancellors.AddAsync(user);
        await dbContext.SaveChangesAsync(true);

        return CreateLoginPayload(auth.Username, user.Email, user.PhoneNumber);
    }

    public async Task<LoginPayloadDto> Register(SchoolAdminRegistrationDataDto registrationDataDto)
    {
        (Auth auth, SchoolAdmin user) = registrationDataDto.CreateModel();
        user.Auth = auth;

        await dbContext.Auths.AddAsync(auth);
        await dbContext.SchoolAdmins.AddAsync(user);
        await dbContext.SaveChangesAsync(true);

        return CreateLoginPayload(auth.Username, user.Email, user.PhoneNumber);
    }

    public async Task<LoginPayloadDto> Register(FacultyRegistrationDataDto registrationDataDto)
    {
        (Auth auth, Faculty user) = registrationDataDto.CreateModel();
        user.Auth = auth;

        await dbContext.Auths.AddAsync(auth);
        await dbContext.Faculties.AddAsync(user);
        await dbContext.SaveChangesAsync(true);

        return CreateLoginPayload(auth.Username, user.Email, user.PhoneNumber);
    }

    public async Task<LoginPayloadDto> Register(StudentRegistrationDataDto registrationDataDto)
    {
        (Auth auth, Student user) = registrationDataDto.CreateModel();
        user.Auth = auth;

        await dbContext.Auths.AddAsync(auth);
        await dbContext.Students.AddAsync(user);
        await dbContext.SaveChangesAsync(true);

        return CreateLoginPayload(auth.Username, user.Email, user.PhoneNumber);
    }
}
