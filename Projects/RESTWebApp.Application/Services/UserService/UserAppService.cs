using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.UserService.Dto;
using RESTWebApp.Data.Databases;
using RESTWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.UserService
{
    public class UserAppService: IUserAppService, IDisposable
    {
        private readonly HumanResourcesContext _databaseContext;
        private IMapper? _mapper;

        public UserAppService(HumanResourcesContext databaseContext, IMapper mapper)
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
                        var newUser = _mapper.Map<MstUser>(model);
                        newUser.PasswordSalt = model.SurName;
                        newUser.Password = CryptographyHelper.GenerateHashWithSalt(model.Password, newUser.PasswordSalt);

                        _databaseContext.Users.Add(newUser);
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
