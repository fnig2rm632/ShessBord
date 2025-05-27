using System.Threading.Tasks;
using ShessBord.DTO.Authentication;
using WebTest.Dtos.Authification;

namespace ShessBord.Interfaces;

public interface IAuthApiClient
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task RevokeToken(string userId);
}