using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ShessBord.DTO.Friend;
using ShessBord.DTO.Game;
using ShessBord.DTO.User;
using ShessBord.Interfaces;
using ShessBord.Models;
using WebTest.Dtos.Authification;

namespace ShessBord.Clients;

public class MatchmakingApiClient : ApiClientBase, IMatchmakingApiClient
{
    public MatchmakingApiClient(HttpClient httpClient)
        : base(httpClient, "api/matchmaking")
    {
        
    }
    public async Task<UserRequestDto> StartSearch(GameResponseDto request)
    {
        return await PostAsync<UserRequestDto>("start-search", request);
    }
    
    public async Task<UserRequestDto> Search(GameResponseDto request)
    {
        return await PostAsync<UserRequestDto>("search", request);
    }
    
    public async Task<bool> Cancel(GameResponseDto request)
    {
        return await PostAsync<bool>("cancel", request);
    }

    public async Task<UserRequestDto> StartMatchmaking(GameResponseDto request)
    {
        return await GetAsync<UserRequestDto>("start", request);
    }
}
