using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTO.Request;
using Models.DTO.Response;
using Models.DTO.Response.ResponseBase;
using Services.Services.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet("UserActive")]
        public async Task<ActionResult<ResponseBase<List<UserResponseDto>>>> GetAllUserActive()
        => await _userService.GetAllUserActive();
        [HttpGet("Name")]
        public async Task<ActionResult<ResponseBase<List<string>>>> GetAllNameUser()
        => await _userService.GetAllNameUser();
        [HttpGet("UserRol")]
        public async Task<ActionResult<ResponseBase<List<UserResponseDto>>>> GetAllUserByRol(string nameRol)
        => await _userService.GetAllUserByRol(nameRol);
        [HttpPost("Registro")]
        public async Task<ActionResult<ResponseBase<bool>>> AddUser(UserRequestDto dto)
        => await _userService.AddUser(dto);
        [HttpPut]
        public async Task<ActionResult<ResponseBase<bool>>> UpdateUser(UserRequestUpdateDto dto)
        => await _userService.UpdateUser(dto);

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseBase<string>>> Login([FromBody] LoginDto loginDto)
            => await _userService.Login(loginDto);
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteUser(int Id)
            => await _userService.DeleteUser(Id);
    }
}
