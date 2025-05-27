using System.Collections.Generic;
using System.Threading.Tasks;
using ShessBord.DTO.Friend;
using ShessBord.Models;

namespace ShessBord.Interfaces;

public interface IFriendApiClient
{
    Task<ServiceResponse<List<FriendResponseDto>>> GetFriendsList(string userId, string? jwtToken = null);
    Task<ServiceResponse<List<FriendResponseDto>>> FindFriendsList(FriendResponseDto responseDto, string? jwtToken = null);
    Task<ServiceResponse<bool?>> SendFriendRequest(FriendResponseDto responseDto, string? jwtToken = null);
    Task<ServiceResponse<bool?>> AcceptFriendRequest(FriendResponseDto responseDto, string? jwtToken = null);
    Task<ServiceResponse<bool?>> DeleteFriendRequest(FriendResponseDto responseDto, string? jwtToken = null);

}