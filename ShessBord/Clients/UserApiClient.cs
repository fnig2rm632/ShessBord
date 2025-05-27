using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ShessBord.DTO.User;
using ShessBord.Interfaces;

namespace ShessBord.Clients;

public class UserApiClient : ApiClientBase, IUserApiClient
{
    public UserApiClient(HttpClient httpClient)
        : base(httpClient, "api/users")
    {
        
    }

    public async Task<UserRequestDto?> GetUser(string userId, string accessToken)
    {
        return await GetAsync<UserRequestDto>($"user{userId}",null!, accessToken);
    }

    public async Task<UserRequestDto?> GetUserProfile(string userId, string accessToken)
    {
        return await GetAsync<UserRequestDto>($"profile{userId}",null!, accessToken);
    }
}