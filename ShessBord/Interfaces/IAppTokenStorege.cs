namespace ShessBord.Interfaces;

public interface IAppTokenStorage
{
    void SaveTokens(string accessToken, string refreshToken, string userId);
    (string AccessToken, string RefreshToken, string userId) GetTokens();
    void ClearTokens();
}