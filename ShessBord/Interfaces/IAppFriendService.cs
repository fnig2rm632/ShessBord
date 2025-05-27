using System.Collections.Generic;
using System.Threading.Tasks;
using ShessBord.DTO.Friend;
using ShessBord.Models;

namespace ShessBord.Interfaces;

public interface IAppFriendService
{
    Task<ServiceResponse<List<FriendResponseDto>>> GetFriendsList();
    Task<ServiceResponse<List<FriendResponseDto>>> FindFriendsList(string query);
    Task<ServiceResponse<bool?>> SendFriendRequest(string friendId);
    Task<ServiceResponse<bool?>> AcceptFriendRequest(string friendId);
    Task<ServiceResponse<bool?>> DeleteFriendRequest(string friendId);
}