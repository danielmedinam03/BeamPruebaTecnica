using Microsoft.EntityFrameworkCore;
using Models.DTO.Request;
using Models.DTO.Response;
using Models.Models;
using Repositories.IRepositories;
using Repositories.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepository : BaseRepository<long, User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> DeleteUserById(long Id)
        {
            
            var data = await GetAsync(x => x.UserId == Id);

            data.Eliminado = true;

            await UpdateAsync(data);
            
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<List<UserResponseDto>> GetAllUserActive() => 
            //_unitOfWork.GetSet<int, UserResponseDto>().FromSqlInterpolated($"EXEC UsersActive;").ToList();
            _unitOfWork.GetSet<int, UserResponseDto>().FromSqlRaw($"EXEC UsersActive;").ToList();
        public async Task<List<UserResponseDto>> GetAllUserByNameRol(string rol) => 
            //_unitOfWork.GetSet<int, UserResponseDto>().FromSqlInterpolated($"EXEC UsersActive;").ToList();
            _unitOfWork.GetSet<int, UserResponseDto>().FromSqlRaw($"EXEC spUsuariosPorNombreRol {rol};").ToList();

        public async Task<List<string>> GetAllName()
        {
            try
            {
                var dat = _unitOfWork.GetSet<long, UserName>().FromSqlInterpolated($"EXEC AllName;").ToList();
                return dat.Select(x => x.Nombre).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddUserSp(User request) 
        {
            try
            {
                //var data = _unitOfWork.GetSet<long, User>().FromSqlRaw($"EXEC AddUser @Nombre={request.Nombre} @Apellido={request.Apellidos} @NombreUsuario={request.NombreUsuario} @Contrasena={request.Contraseña} @FechaNacimiento={request.FechaNacimiento} @Celular={request.Celular} @Estado={request.Estado} @Rol={request.RolId} @Eliminado={false}");
                _unitOfWork.GetContext().Database.ExecuteSqlInterpolated($"EXEC AddUser @Nombre={request.Nombre}, @Apellido={request.Apellidos}, @NombreUsuario={request.NombreUsuario}, @Contrasena={request.Contraseña}, @FechaNacimiento={request.FechaNacimiento}, @Celular={request.Celular}, @Estado={request.Estado}, @Rol={request.RolId}, @Eliminado={false}");
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<bool> DeleteUserSp(int id) 
        {
            try
            {
                _unitOfWork.GetContext().Database.ExecuteSqlInterpolated($"EXEC DeleteUser @Id={id}, @FechaEliminacion={DateTime.Now}");
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> UpdateUserSp(User request)
        {
            try
            {
                _unitOfWork.GetContext().Database.ExecuteSqlInterpolated($"EXEC UpdateUser @Id={request.UserId}, @Nombre={request.Nombre}, @Apellido={request.Apellidos}, @NombreUsuario={request.NombreUsuario}, @Contrasena={request.Contraseña}, @FechaNacimiento={request.FechaNacimiento}, @Celular={request.Celular}, @Estado={request.Estado}, @Rol={request.RolId}, @Eliminado={false}");
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
