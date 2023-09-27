namespace TravelKGServices.Services.Implementation;

public class UserAuthenticationService : IAuthenticationService<AuthUserResponse>
{
    public UserAuthenticationService(TravelKGContext context)
    {
        _context = context;
        _random = new Random();
    }

    private readonly TravelKGContext _context;
    Random _random;

    public async Task<AuthUserResponse> AuthenticateAsync(AuthRequest request)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password);

            if (user == null)
            {
                return new AuthUserResponse
                {
                    StatusCode = 400,
                    ResponseMessage = "Пользователь не найден, пожалуйста введите правильный логин или пароль!"
                };
            }

            var token = GenerateJwtToken(user);

            return new AuthUserResponse
            {
                StatusCode = 200,
                ResponseMessage = "Вход выполнен успешно",
                UserId = user.UserId,
                UserName = user.UserName,
                AccessToken = token
            };


        }
        catch (Exception ex)
        {
            return new AuthUserResponse
            {
                StatusCode = 500,
                ResponseMessage = ex.Message
            };
        }
    }

    public async Task<PasswordReset> ResetPasswordAsync(string userName)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return new PasswordReset
                {
                    Success = false
                };

            int oneTimeCode = GenerateCode();
            SendEmail(user.Email ?? "", oneTimeCode);

            return new PasswordReset
            {
                Success = true,
                OneTimeCode = oneTimeCode
            };
        }
        catch
        {
            return new PasswordReset { Success = false };
        }

    }

    public async Task UpdatePasswordAsync(UserResetPassword userRP)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userRP.UserName);
            if (user != null)
            {
                user.Password = userRP.PasswordNew;
                await _context.SaveChangesAsync();
            }
        }
        catch { }
    }


    #region ServiceMethods

    int GenerateCode()
    {
        return _random.Next(1000, 9999);
    }

    void SendEmail(string email, int oneTimeCode)
    {
        var smtpClient = new SmtpClient("smtp.mail.ru")
        {
            Port = 587,
            Credentials = new NetworkCredential("travelkg_helpdesc", "d86yBzyzZZ5WZaNZVZpb"),
            EnableSsl = true,
        };

        string subject = "Служба технической поддержки MedLink";
        string body = $"Код для изменения вашего пароля: {oneTimeCode}";

        smtpClient.Send("travelkg_helpdesc@mail.ru", email, subject, body);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = AuthOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserName", user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim("Email", (user.Email ?? "").Trim()),
        };

        var token = new JwtSecurityToken(AuthOptions.ISSUER,
            AuthOptions.AUDIENCE,
            claims,
            expires: DateTime.Now.AddMinutes(AuthOptions.LIFE_TIME),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion
}
