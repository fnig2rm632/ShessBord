using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShessBord.DTO.Friend;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Services;

public class AppFriendService(IAppTokenStorage appTokenStorage, IFriendApiClient friendApiClient) : IAppFriendService
{
    public async Task<ServiceResponse<List<FriendResponseDto>>> GetFriendsList()
    {
        try
        {
            var storege = appTokenStorage.GetTokens();
        
            return await friendApiClient.GetFriendsList(storege.userId,storege.AccessToken);

        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<FriendResponseDto>>()
            {
                Data = null!,
                Success = false,
                Message = $"Error getting friends list {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<List<FriendResponseDto>>> FindFriendsList(string query)
    {
        try
        {
            var storege = appTokenStorage.GetTokens();
        
            var friendResponseDto = new FriendResponseDto()
            {
                FriendId = "friendId",
                MyId = storege.userId,
                Status = "Deleted", 
                FriendName = "Unknown",
                Query = query
            };
            
            return await friendApiClient.FindFriendsList(friendResponseDto,storege.AccessToken);

        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<FriendResponseDto>>()
            {
                Data = null!,
                Success = false,
                Message = $"Error getting friends list {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<bool?>> SendFriendRequest(string friendId)
    {
        try
        {
            var storege = appTokenStorage.GetTokens();

            var friendResponseDto = new FriendResponseDto()
            {
                FriendId = storege.userId,
                MyId = friendId,
                Status = "Deleted", 
                FriendName = "Unknown",
                Query = ""
            };
        
            return await friendApiClient.SendFriendRequest(friendResponseDto,storege.AccessToken);

        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool?>()
            {
                Data = false,
                Success = false,
                Message = $"Error sending friend request {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<bool?>> AcceptFriendRequest(string friendId)
    {
        try
        {
            var storege = appTokenStorage.GetTokens();

            var friendResponseDto = new FriendResponseDto()
            {
                FriendId = friendId,
                MyId = storege.userId,
                Status = "Deleted", 
                FriendName = "Unknown",
                Query = ""
            };
            
            return await friendApiClient.AcceptFriendRequest(friendResponseDto,storege.AccessToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ServiceResponse<bool?>()
            {
                Data = false,
                Success = false,
                Message = $"Error accepting friend request {ex.Message}"
            };
        }
    }

    public async Task<ServiceResponse<bool?>> DeleteFriendRequest(string friendId)
    {
        try
        {
            var storege = appTokenStorage.GetTokens();

            var friendResponseDto = new FriendResponseDto()
            {
                FriendId = friendId,
                MyId = storege.userId,
                Status = "Deleted", 
                FriendName = "Unknown",
                Query = ""
            };
        
            var response = await friendApiClient.DeleteFriendRequest(friendResponseDto, storege.AccessToken);
            Console.WriteLine($"Status: {response.Success}, Message: {response.Message}");
            return response;

        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool?>()
            {
                Data = false,
                Success = false,
                Message = $"Error deleting friends list {ex.Message}"
            };
        }
    }
}