using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Media;
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

    public async Task<GameResponseDto> PostStartedGameAsync()
    {
        try
        {
            var mainUser = appTokenStorage.GetTokens();
            var game = new GameResponseDto
            {
                PlayerWhiteId = mainUser.userId,
                PlayerBlackId = mainUser.userId,
            };
            var tmp = await gameApiClient.PostStartedGame(game, mainUser.AccessToken);
            return tmp;
        }
        catch (Exception e)
        {
            return new GameResponseDto
            {
                Id = 0
            };
        }
    }

    public async Task<bool> FinishGameAsync(GameResponseDto? item)
    {
        try
        {
            var mainUser = appTokenStorage.GetTokens();
            var tmp = await gameApiClient.FinishGame(item,mainUser.AccessToken);
            return tmp;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}