using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ShessBord.DTO.Friend;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Clients;

public class FriendApiClient : ApiClientBase , IFriendApiClient
{
     public FriendApiClient(HttpClient httpClient)
            : base(httpClient, "api/friends")
        {
            
        }


        public async Task<ServiceResponse<List<FriendResponseDto>>> GetFriendsList(string userId, string? jwtToken = null)
        {
            return await GetAsync<ServiceResponse<List<FriendResponseDto>>>($"all{userId}",null,jwtToken);
        }

        public async Task<ServiceResponse<List<FriendResponseDto>>> FindFriendsList(FriendResponseDto responseDto, string? jwtToken = null)
        {
            return await PostAsync<ServiceResponse<List<FriendResponseDto>>>($"find-friend",responseDto,jwtToken);
        }

        public async Task<ServiceResponse<bool?>> SendFriendRequest(FriendResponseDto responseDto, string? jwtToken = null)
        {
            
            return await PostAsync<ServiceResponse<bool?>>($"send-request",responseDto,jwtToken);
        }

        public async Task<ServiceResponse<bool?>> AcceptFriendRequest(FriendResponseDto responseDto, string? jwtToken = null)
        { 
            return await PostAsync<ServiceResponse<bool?>>($"accept-request",responseDto,jwtToken);
        }

        public async Task<ServiceResponse<bool?>> DeleteFriendRequest(FriendResponseDto responseDto, string? jwtToken = null)
        {
            return await DeleteAsync<ServiceResponse<bool?>>($"delete-request",responseDto,jwtToken);
        }
}