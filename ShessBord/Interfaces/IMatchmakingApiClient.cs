using System.Threading.Tasks;
using ShessBord.DTO.Game;
using ShessBord.DTO.User;

namespace ShessBord.Interfaces;

public interface IMatchmakingApiClient
{
    Task<UserRequestDto> StartSearch(GameResponseDto request);
    Task<UserRequestDto> Search(GameResponseDto request);
    Task<bool> Cancel(GameResponseDto request);
    Task<UserRequestDto> StartMatchmaking(GameResponseDto request);
}