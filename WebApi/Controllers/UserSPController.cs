using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Response.ResponseBase;
using Models.DTO.Response;
using Services.Services.User;
using Models.DTO.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSPController : ControllerBase
    {
        private readonly IUserSPService _userSPService;

        public UserSPController(IUserSPService userSPService)
        {
            _userSPService = userSPService;
        }

        [HttpGet("Name")]
        public async Task<ActionResult<ResponseBase<List<string>>>> GetAllName()
        => await _userSPService.GetAllName();
        [HttpGet("UserActive")]
        public async Task<ActionResult<ResponseBase<List<UserResponseDto>>>> GetAllUserActive()
        => await _userSPService.GetAllUserActive();
        [HttpGet("UserByRol")]
        public async Task<ActionResult<ResponseBase<List<UserResponseDto>>>> GetAllUserByRol(string rol)
        => await _userSPService.GetAllUserByNameRol(rol);

        [HttpPost("Registro")]
        public async Task<ActionResult<ResponseBase<bool>>> AddUser(UserRequestDto dto)
        => await _userSPService.AddUserSP(dto);
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteUser(int Id)
        => await _userSPService.DeleteUserSp(Id);
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseBase<string>>> Login([FromBody] LoginDto loginDto)
            => await _userSPService.Login(loginDto);

        [HttpPut]
        public async Task<ActionResult<ResponseBase<bool>>> UpdateUser(UserRequestUpdateDto dto)
        => await _userSPService.UpdateUser(dto);
    }
}
