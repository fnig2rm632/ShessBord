using System.Collections.Generic;
using System.Threading.Tasks;
using ShessBord.DTO.Authentication;
using ShessBord.DTO.Game;
using ShessBord.Models;

namespace ShessBord.Interfaces;

public interface IGameApiClient
{
    Task<ServiceResponse<List<GameResponseDto>>> GetGameList(string userId, string accessToken);
    Task<GameResponseDto> PostStartedGame(GameResponseDto? item, string accessToken);
}