using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShessBord.DTO.Game;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Services;

public class AppGameService(IGameApiClient gameApiClient, IAppTokenStorage appTokenStorage) : IAppGameService
{
    public async Task<ServiceResponse<List<GameResponseDto>>> GetUserProfileAsync()
    {
        try
        {
            var mainUser = appTokenStorage.GetTokens();
            return await gameApiClient.GetGameList(mainUser.userId, mainUser.AccessToken);
        }
        catch (Exception e)
        {
            return new ServiceResponse<List<GameResponseDto>>
            {
                Message = "Нету пользователя",
            };
        }
    }
}