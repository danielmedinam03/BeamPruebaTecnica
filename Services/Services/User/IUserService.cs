using Models.DTO.Request;
using Models.DTO.Response;
using Models.DTO.Response.ResponseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.User
{
    public interface IUserService
    {
        Task<ResponseBase<bool>> AddUser(UserRequestDto request);
        Task<ResponseBase<bool>> UpdateUser(UserRequestUpdateDto request);
        Task<ResponseBase<bool>> DeleteUser(int requestId);
        Task<ResponseBase<List<UserResponseDto>>> GetAllUserActive();
        Task<ResponseBase<List<string>>> GetAllNameUser();
        Task<ResponseBase<List<UserResponseDto>>> GetAllUserByRol(string request);
        Task<ResponseBase<Models.Models.User>> GetAysncByUserName(string userName);
        Task<ResponseBase<string>> Login(LoginDto loginDto);

    }
}
