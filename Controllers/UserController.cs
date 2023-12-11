using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Models;
using RESTWebApp.Application.Services.LoginService;
using RESTWebApp.Application.Services.UserService;
using RESTWebApp.Application.Services.UserService.Dto;
using RESTWebApp.Example.Models;

namespace RESTWebApp.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private IMapper? _mapper;

        public UserController(IUserAppService userAppService, IMapper mapper)
        {
            _userAppService = userAppService;
            _mapper = mapper;
        }

        [HttpDelete("DeleteUser")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var (isDeleted, isMessage) = await _userAppService.Delete(Id);
                if (!isDeleted)
                {
                    return Requests.Response(this, new ApiStatus(406), "Error", "Error");
                }

                return Requests.Response(this, new ApiStatus(200), "Success", "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("SaveUser")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveUser([FromBody] UserModel model)
        {
            try
            {
                var userModel = _mapper.Map<CreateUserDto>(model);

                var (isAdded, isMessage) = await _userAppService.Create(userModel);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), "Error", "Error");
                }

                return Requests.Response(this, new ApiStatus(200), "Success", "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("UpdateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            try
            {
                var userModel = _mapper.Map<UpdateUserDto>(model);
                var (isUpdated, isMessage) = await _userAppService.Update(userModel);
                if (!isUpdated)
                {
                    return Requests.Response(this, new ApiStatus(406), "Error", "Error");
                }

                return Requests.Response(this, new ApiStatus(200), "Success", "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}
