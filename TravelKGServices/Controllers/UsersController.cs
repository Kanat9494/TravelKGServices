namespace TravelKGServices.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    public UsersController(IAuthenticationService<AuthUserResponse> authenticationService)
    {
        _authenticationService = authenticationService;
    }

    private readonly IAuthenticationService<AuthUserResponse> _authenticationService;



    [HttpGet("Test")]
    [AllowAnonymous]
    public IActionResult Test()
        => Ok("Работает");
}
