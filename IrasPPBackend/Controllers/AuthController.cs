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
    public async Task<IActionResult> Login([FromBody] AuthDto auth)
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

    [HttpPost("register/admin")]
    public async Task<IActionResult> Register([FromBody] AdminRegistrationDataDto registrationData)
    {
        try
        {
            var loginPayload = await authService.Register(registrationData);
            return Ok(loginPayload);
        }
        catch
        {
            return BadRequest("Failed to create account");
        }
    }

    // [HttpPost("register/vc")]
    // public async Task<IActionResult> Register([FromBody] ViceChancellorRegistrationDataDto registrationData)
    // {

    // }

    // [HttpPost("register/schooladmin")]
    // public async Task<IActionResult> Register([FromBody] SchoolAdminRegistrationDataDto registrationData)
    // {

    // }

    // [HttpPost("register/faculty")]
    // public async Task<IActionResult> Register([FromBody] FacultyRegistrationDataDto registrationData)
    // {

    // }

    // [HttpPost("register/student")]
    // public async Task<IActionResult> Register([FromBody] StudentRegistrationDataDto registrationData)
    // {

    // }
}
