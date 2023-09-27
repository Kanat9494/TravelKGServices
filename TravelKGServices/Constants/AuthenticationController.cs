namespace TravelKGServices.Constants;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(IAuthenticationService<AuthUserResponse> authenticationService)
    {
        _authenticationService = authenticationService;
    }

    private readonly IAuthenticationService<AuthUserResponse> _authenticationService;


    [HttpPost("AuthenticateUser")]
    [AllowAnonymous]
    public async Task<AuthUserResponse> AuthenticateUser([FromBody] AuthRequest request)
    {
        return await _authenticationService.AuthenticateAsync(request: request);
    }

    [HttpGet("Test")]
    public IActionResult Test()
        => Ok("Работает");
}
