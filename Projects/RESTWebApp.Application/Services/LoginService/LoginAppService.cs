using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTWebApp.Application.Exceptions;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.UserService.Dto;
using RESTWebApp.Data.Databases;
using RESTWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.LoginService
{
    public class LoginAppService : ILoginAppService, IDisposable
    {
        private readonly HumanResourcesContext _databaseContext;
        private IMapper? _mapper;

        public LoginAppService(HumanResourcesContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Login(UserDto model)
        {
            try
            {
                var user = _databaseContext.Users.FirstOrDefault(w => w.UserName == model.UserName);

                if (user != null)
                {
                    string Password = CryptographyHelper.GenerateHashWithSalt(model.Password, user.PasswordSalt);
                    user = _databaseContext.Users.FirstOrDefault(w => w.UserName == model.UserName && w.Password.Equals(Password));

                    return await Task.Run(() => user != null
                            ? _mapper.Map<UserDto>(user)
                            : new UserDto());
                }
                else
                {
                    return await Task.Run(() => new UserDto());
                }
            }
            catch (UserException ex)
            {
                throw new UserException(ex.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _databaseContext?.Dispose();
                _mapper = null;
            }
        }
    }
}
