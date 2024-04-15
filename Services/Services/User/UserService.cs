using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO.Request;
using Models.DTO.Response;
using Models.DTO.Response.ResponseBase;
using Models.Models;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 
        private readonly IConfiguration _configuration;
        private readonly ILoginEventRepository _loginEventRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, ILoginEventRepository loginEventRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _loginEventRepository = loginEventRepository;
        }

        public async Task<ResponseBase<bool>> AddUser(UserRequestDto request)
        {
            try
            {
                var hasher = new PasswordHasher<Models.Models.User>();


                Models.Models.User user = new Models.Models.User()
                {
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    Celular = request.Celular,
                    RolId = request.RolId,
                    FechaNacimiento = request.FechaNacimiento,
                    Estado = true,
                    NombreUsuario = request.NombreUsuario,
                    Contraseña = hasher.HashPassword(null, request.Contraseña),
                    Eliminado = false,
                };

                await _userRepository.AddAsync(user);

                return new ResponseBase<bool>(System.Net.HttpStatusCode.Created,"OK",data: true);

            }catch (Exception ex)
            {
                return new ResponseBase<bool>(System.Net.HttpStatusCode.BadRequest,message:ex.Message);
            }
        }

        public async Task<ResponseBase<bool>> DeleteUser(int requestId)
        {
            try
            {
                await _userRepository.DeleteUserById(requestId);
                return new ResponseBase<bool>(System.Net.HttpStatusCode.OK,"Eliminado correctamente",true);

            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }

        }

        public async Task<ResponseBase<List<string>>> GetAllNameUser()
        {
            try
            {
                var data = (await _userRepository.GetAllAsync(filter: x => x.Eliminado == false, includeProperties:"Rol")).ToList();
                if (data == null)
                {
                    return new ResponseBase<List<string>>(System.Net.HttpStatusCode.OK, "No existen datos", new List<string>());
                }
                var nombresCompletos = data.Select(x => $"{x.Nombre} {x.Apellidos}").ToList();

                return new ResponseBase<List<string>>(System.Net.HttpStatusCode.OK, "Ok", nombresCompletos);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<string>>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }

        }

        public async Task<ResponseBase<List<UserResponseDto>>> GetAllUserActive()
        {
            try
            {
                var data = (await _userRepository.GetAllAsync(filter: x => x.Estado == true 
                    && x.Eliminado == false,includeProperties: "Rol")).ToList();
                if (data == null)
                {
                    return new ResponseBase<List<UserResponseDto>>(System.Net.HttpStatusCode.OK, "No existen datos", new List<UserResponseDto>());
                }
                return new ResponseBase<List<UserResponseDto>>(System.Net.HttpStatusCode.OK, "Ok", MapUserToUserResponseDto(data), count: MapUserToUserResponseDto(data).Count);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<UserResponseDto>>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }
        }
        //No lo pude mapear por Automapper
        private List<UserResponseDto> MapUserToUserResponseDto(List<Models.Models.User> users)
        {
            List<UserResponseDto> listResponse = new List<UserResponseDto>();

            foreach (var item in users)
            {

                listResponse.Add(new UserResponseDto
                {
                    Id = item.UserId,
                    Nombre = item.Nombre,
                    Apellidos = item.Apellidos,
                    Celular = item.Celular,
                    NombreUsuario = item.NombreUsuario,
                    Estado = item.Estado,
                    Contraseña = item.Contraseña,
                    FechaNacimiento = item.FechaNacimiento,
                    Rol = item.Rol.Nombre
                });
            }
            return listResponse;
        }
        public async Task<ResponseBase<List<UserResponseDto>>> GetAllUserByRol(string request)
        {
            try
            {
                var data = (await _userRepository.GetAllAsync(filter: x => x.Eliminado == false && 
                    x.Rol.Nombre.ToLower().Trim() == request.ToLower().Trim(), includeProperties: "Rol")).ToList();
                if (data == null)
                {
                    return new ResponseBase<List<UserResponseDto>>(System.Net.HttpStatusCode.OK, "No existen datos", new List<UserResponseDto>());
                }
                return new ResponseBase<List<UserResponseDto>>(System.Net.HttpStatusCode.OK, "Ok", MapUserToUserResponseDto(data), MapUserToUserResponseDto(data).Count);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<UserResponseDto>>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }
        }

        public async Task<ResponseBase<bool>> UpdateUser(UserRequestUpdateDto request)
        {
            try
            {
                var data = await _userRepository.GetAsync(x => x.UserId == request.UserId);

                if (data == null)
                {
                    return new ResponseBase<bool>(System.Net.HttpStatusCode.NotFound, "No existe un usuario con ese id", data: true);
                }
                else
                {

                    if (request.UserId != null && request.UserId != 0)
                    {

                        var hasher = new PasswordHasher<Models.Models.User>();

                        data.Nombre = request.Nombre;
                        data.Apellidos = request.Apellidos;
                        data.Celular = request.Celular;
                        data.RolId = request.RolId;
                        data.FechaNacimiento = request.FechaNacimiento;
                        data.Estado = true;
                        data.NombreUsuario = request.NombreUsuario;
                        data.Contraseña = hasher.HashPassword(null, request.Contraseña);
                        data.Eliminado = false;

                        await _userRepository.UpdateAsync(data);
                        return new ResponseBase<bool>(System.Net.HttpStatusCode.Created, "OK", data: true);

                    }
                    else
                    {
                        return new ResponseBase<bool>(System.Net.HttpStatusCode.NotFound, "El id no puede ser nulo o valor 0", data: true);
                    }
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }
        }

        public async Task<ResponseBase<Models.Models.User>> GetAysncByUserName(string userName)
        {
            try
            {
                var data = await _userRepository.GetAsync(predicate: x => x.NombreUsuario == userName);
                if (data == null)
                {
                    return new ResponseBase<Models.Models.User>(System.Net.HttpStatusCode.OK, "No existen datos", new Models.Models.User());
                }
                return new ResponseBase<Models.Models.User>(System.Net.HttpStatusCode.OK, "Ok",data,1);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Models.Models.User>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }
        }
        public async Task<ResponseBase<string>> Login(LoginDto loginDto)
        {
            try
            {
                var user = (await GetAysncByUserName(loginDto.UserName)).Data;

                if (user != null)
                {
                    var hasher = new PasswordHasher<Models.Models.User>();
                    var result = hasher.VerifyHashedPassword(user, user.Contraseña, loginDto.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        var data = GenerateJwtToken(user);
                        LoginEvent loginEvent1 = new LoginEvent()
                        {
                            UserId = user.UserId,
                            HoraIngreso = DateTime.Now,
                            Resultado = true
                        };
                        await _loginEventRepository.AddAsync(loginEvent1);

                        return new ResponseBase<string>(System.Net.HttpStatusCode.OK, "Ok", data, 1);

                    }
                    LoginEvent loginEvent = new LoginEvent()
                    {
                        UserId = user.UserId,
                        HoraIngreso = DateTime.Now,
                        Resultado = false

                    };

                    await _loginEventRepository.AddAsync(loginEvent);

                }
                return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, message: "No existe el usuario!");


            }
            catch (Exception ex)
            {
                return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, message: ex.Message);
            }

        }
        private string GenerateJwtToken(Models.Models.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.NombreUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
