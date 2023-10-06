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

namespace IrasPPBackend.Services;

public interface IAuthService
{
    Task<LoginPayloadDto> Login(Auth auth);
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

    public async Task<LoginPayloadDto> Login(Auth auth)
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
                var jwt = EncodeJwt(authObj.Username, userObj.Email, userObj.PhoneNumber);

                return new LoginPayloadDto()
                {
                    JwtToken = jwt
                };
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
}
