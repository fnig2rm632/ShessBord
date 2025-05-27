using System;
using System.Threading.Tasks;
using ShessBord.DTO.User;
using ShessBord.Interfaces;

namespace ShessBord.Services;

public class AppUserService(IUserApiClient userApiClient,IAppTokenStorage appTokenStorage) : IAppUserService
{
    public async Task<UserRequestDto?> GetUserProfileAsync(string userId)
    {
        try
        {
            var mainUser = appTokenStorage.GetTokens();
            return await userApiClient.GetUserProfile(userId, mainUser.AccessToken);
        }
        catch (Exception e)
        {
            return new UserRequestDto()
            {
                Error = "Нету пользователя",
                Username = "No User Found",
            };
        }
        
    }

    public async Task<UserRequestDto?> GetUserAsync(string userId)
    {
        try
        {
            var tmp = await userApiClient.GetUser(userId);
            return tmp;
        }
        catch (Exception e)
        {
            return new UserRequestDto()
            {
                Error = "Нету пользователя",
                Username = "No User Found",
            };
        }
    }

    public async Task<UserRequestDto?> GetMainUserAsync()
    {
        var mainUser = appTokenStorage.GetTokens();
        return await GetUserProfileAsync(mainUser.userId);
    }
}