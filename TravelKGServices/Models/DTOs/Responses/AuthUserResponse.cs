namespace TravelKGServices.Models.DTOs.Responses;

public class AuthUserResponse : BaseResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string AccessToken { get; set; }
}
