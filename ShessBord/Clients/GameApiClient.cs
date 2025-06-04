using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ShessBord.DTO.Game;
using ShessBord.DTO.User;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Clients;

public class GameApiClient : ApiClientBase, IGameApiClient
{
    public GameApiClient(HttpClient httpClient)
        : base(httpClient, "api/game")
    {
        
    }

    public async Task<ServiceResponse<List<GameResponseDto>>> GetGameList(string userId, string accessToken)
    {
        return await GetAsync<ServiceResponse<List<GameResponseDto>>>($"all{userId}",null!,accessToken);
    }
    
    public async Task<GameResponseDto> PostStartedGame(GameResponseDto? item,string accessToken)
    {
        var tmp =  await PostAsync<GameResponseDto>("game",item,accessToken);
        return tmp;
    }
}