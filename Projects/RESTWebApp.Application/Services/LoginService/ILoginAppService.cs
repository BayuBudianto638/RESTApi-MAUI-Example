using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.LoginService
{
    public interface ILoginAppService
    {
        Task<UserDto> Login(UserDto model);
    }
}
