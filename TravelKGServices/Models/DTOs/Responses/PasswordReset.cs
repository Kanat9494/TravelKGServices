namespace TravelKGServices.Models.DTOs.Responses;

public class PasswordReset
{
    public bool Success { get; set; }
    public int OneTimeCode { get; set; }
}
