using System;
using System.Net.Http;
using System.Threading.Tasks;
using ShessBord.DTO.Game;
using ShessBord.DTO.User;
using ShessBord.Interfaces;

namespace ShessBord.Services;

public class AppMatchmakingService(IMatchmakingApiClient matchmakingApiClient,IAppTokenStorage appTokenStorage) : IAppMatchmakingService
{
    public async Task<bool> StartSearch(string type, int size)
    {
        try
        {
            var game = new GameResponseDto
            {
                PlayerWhiteId = appTokenStorage.GetTokens().userId,
                BoardSize = size,
                Type = type,
                PlayerBlackId = appTokenStorage.GetTokens().userId
            };
            var tmp = await matchmakingApiClient.StartSearch(game);

            return !string.IsNullOrEmpty(tmp.Username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> Search(string type, int size)
    {
        try
        {
            var game = new GameResponseDto
            {
                PlayerWhiteId = appTokenStorage.GetTokens().userId,
                BoardSize = size,
                Type = type,
                PlayerBlackId = "none"
            };
            var tmp = await matchmakingApiClient.Search(game);
        
            return !string.IsNullOrEmpty(tmp.Username);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Cancel()
    {
        try
        {
            var game = new GameResponseDto
            {
                PlayerWhiteId = appTokenStorage.GetTokens().userId,
                PlayerBlackId = "none"
            };
            return await matchmakingApiClient.Cancel(game);
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public async Task<UserRequestDto> Start()
    {
        try
        {
            var game = new GameResponseDto
            {
                PlayerWhiteId = appTokenStorage.GetTokens().userId,
                BoardSize = 9,
                Type = "Bullet",
                PlayerBlackId = "none"
            };
            var tmp = await matchmakingApiClient.StartMatchmaking(game);

            return tmp;
        }
        catch (Exception e)
        {
            return new UserRequestDto
            {
                Error = e.Message
            };
        }
    }
}

