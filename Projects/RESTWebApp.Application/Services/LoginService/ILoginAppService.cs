using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.LoginService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.LoginService
{
    public interface ILoginAppService
    {
        Task<(bool, string)> Create(CreateUserDto model);
        Task<(bool, string)> Update(UpdateUserDto model);
        Task<(bool, string)> Delete(int id);
        Task<UserDto> Login(UserDto model);
    }
}
