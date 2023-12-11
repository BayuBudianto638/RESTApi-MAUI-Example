using RESTWebApp.Application.Services.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.UserService
{
    public interface IUserAppService
    {
        Task<(bool, string)> Create(CreateUserDto model);
        Task<(bool, string)> Update(UpdateUserDto model);
        Task<(bool, string)> Delete(int id);
    }
}
