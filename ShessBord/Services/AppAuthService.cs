using System;
using System.Threading.Tasks;
using ShessBord.Interfaces;
using WebTest.Dtos.Authification;

namespace ShessBord.Services;

public class AppAuthService(IAppTokenStorage appTokenStorage, IAuthApiClient authApiClient) : IAppAuthService
{
    public async Task<bool> TryAutoLoginAsync()
    {
        try
        {
            var dataUser = appTokenStorage.GetTokens();
                
            if (string.IsNullOrEmpty(dataUser.RefreshToken) || string.IsNullOrEmpty(dataUser.userId))
            {
                appTokenStorage.ClearTokens();
                return false;
            }
            
            var response = await authApiClient.RefreshTokenAsync(new RefreshTokenRequestDto
            {
                RefreshToken = dataUser.RefreshToken,
                UserId = dataUser.userId
            });

            if (response == null || !response.Success)
            {
                appTokenStorage.ClearTokens();
                return false;
            }

            appTokenStorage.SaveTokens(response.AccessToken!, response.RefreshToken! , response.UserId!);
                
            return true;
        }
        catch
        {
            appTokenStorage.ClearTokens();
            return false;
        }
    }


    public async Task LogoutAsync()
    {
        try
        {
            string token = appTokenStorage.GetTokens().userId;
            await authApiClient.RevokeToken(token);
            await ClearAuthDataAsync();
        }
        catch (Exception ex)
        {
            await ClearAuthDataAsync();
        }
    }

    private async Task ClearAuthDataAsync()
    {
        await Task.Run(appTokenStorage.ClearTokens);
    }
}