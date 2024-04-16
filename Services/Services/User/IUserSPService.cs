using Microsoft.AspNetCore.Identity;
using Models.DTO.Request;
using Models.DTO.Response;
using Models.DTO.Response.ResponseBase;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.User
{
    public interface IUserSPService
    {
        Task<ResponseBase<List<UserResponseDto>>> GetAllUserActive();
        Task<ResponseBase<List<string>>> GetAllName();
        Task<ResponseBase<List<UserResponseDto>>> GetAllUserByNameRol(string rol);
        Task<ResponseBase<bool>> AddUserSP(UserRequestDto request);
        Task<ResponseBase<bool>> DeleteUserSp(int id);
        Task<ResponseBase<string>> Login(LoginDto loginDto);
        Task<ResponseBase<bool>> UpdateUser(UserRequestUpdateDto request);

    }
}
