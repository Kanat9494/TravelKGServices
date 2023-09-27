namespace TravelKGServices.Services.Interfaces;

public interface IAuthenticationService<TResponse> where TResponse : class
{
    Task<TResponse> AuthenticateAsync(AuthRequest request);
    Task<PasswordReset> ResetPasswordAsync(string userName);
    Task UpdatePasswordAsync(UserResetPassword userRP);
}
