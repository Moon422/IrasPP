using System.Threading.Tasks;
using IrasPPBackend.Services;
using Microsoft.AspNetCore.Mvc;
using IrasPPBackend.Schemas;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Auth auth)
    {
        try
        {
            LoginPayloadDto loginPayload = await this.authService.Login(auth);
            return Ok(loginPayload);
        }
        catch
        {
            return BadRequest("Invalid Credentials");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody])
    {

    }
}
