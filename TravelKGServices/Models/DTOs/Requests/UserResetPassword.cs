namespace TravelKGServices.Models.DTOs.Requests;

public class UserResetPassword
{
    public string UserName { get; set; }
    public string PasswordNew { get; set; }
}
