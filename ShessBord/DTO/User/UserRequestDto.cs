using System.Collections.Generic;
using ShessBord.DTO.Friend;
using ShessBord.DTO.Game;

namespace ShessBord.DTO.User;

public class UserRequestDto
{
    public string Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public List<FriendResponseDto> Friends { get; set; } = new(); 
    public List<GameResponseDto> Games { get; set; } = new(); 
    public bool Success { get; set; }
    public string? Error { get; set; }
}