namespace ShessBord.Models;

public class TokenData
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string UserId { get; set; } = null!;
}