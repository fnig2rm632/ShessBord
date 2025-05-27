using System.Collections.Generic;
using System.Threading.Tasks;
using ShessBord.DTO.Game;
using ShessBord.Models;

namespace ShessBord.Interfaces;

public interface IAppGameService
{
    Task<ServiceResponse<List<GameResponseDto>>> GetUserProfileAsync();
}