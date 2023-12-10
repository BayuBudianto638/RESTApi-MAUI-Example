using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTWebApp.Application.Exceptions;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.LoginService.Dto;
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

        private readonly string privateKey = "ShiawaseSoftware";
        private readonly string iv = "ShiawaseSoftwareCorporat";

        public LoginAppService(HumanResourcesContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateUserDto model)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = _mapper.Map<MstUser>(model);
                        user.Password = CryptographyHelper.Encrypt(model.Password, privateKey, iv);
                        _databaseContext.Users.Add(user);
                        await _databaseContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return await Task.Run(() => (true, "Success"));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, ex.Message));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error create user: {outerEx.Message}"));
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = await _databaseContext.Users.SingleOrDefaultAsync(w => w.Id == id);

                        if (user != null)
                        {
                            _databaseContext.Users.Remove(user);
                            await _databaseContext.SaveChangesAsync();

                            await transaction.CommitAsync();
                            return await Task.Run(() => (true, "User removed successfully"));
                        }
                        else
                        {
                            return await Task.Run(() => (false, "User not found"));
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, $"Error removing user: {ex.Message}"));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error: {outerEx.Message}"));
            }
        }

        public async Task<UserDto> Login(UserDto model)
        {
            try
            {
                var user = await _databaseContext.Users
                    .SingleOrDefaultAsync(w => w.UserName == model.UserName && w.Password == model.Password);

                if (user != null)
                {
                    var password = CryptographyHelper.Decrypt(user.Password, privateKey, iv);

                    return await Task.Run(() => password.Equals(model.Password)
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

        public async Task<(bool, string)> Update(UpdateUserDto model)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = _mapper.Map<MstUser>(model);

                        _databaseContext.Users.Update(user);
                        await _databaseContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return await Task.Run(() => (true, "Success"));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, $"Error updating user: {ex.Message}"));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error: {outerEx.Message}"));
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
