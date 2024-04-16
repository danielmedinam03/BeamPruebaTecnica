using Models.DTO.Request;
using Models.DTO.Response;
using Models.Models;
using Repositories.IRepositories.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IUserRepository : IBaseReporitory<long,User>
    {
        Task<bool> DeleteUserById(long Id);
        Task<List<UserResponseDto>> GetAllUserActive();
        Task<List<string>> GetAllName();
        Task<List<UserResponseDto>> GetAllUserByNameRol(string rol);
        Task<bool> AddUserSp(User request);
        Task<bool> UpdateUserSp(User request);
        Task<bool> DeleteUserSp(int id);

    }
}
