using System;
using System.Net.Http;
using System.Threading.Tasks;
using ShessBord.Clients;
using ShessBord.DTO.Authentication;
using ShessBord.Interfaces;
using WebTest.Dtos.Authification;

namespace ShessBord;

public class AuthApiClient : ApiClientBase, IAuthApiClient
{

    public AuthApiClient(HttpClient httpClient)
        : base(httpClient, "api/auth")
    {
        
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        return await PostAsync<AuthResponseDto>("login", request);
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request)
    {
        return await PostAsync<AuthResponseDto>("register", request);
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        return await PostAsync<AuthResponseDto>("refresh-token", request);
    }
    
    public async Task RevokeToken(string userId)
    {
        await PostAsync<object>($"revoke-token?userId={Uri.EscapeDataString(userId)}", null);
    }
}