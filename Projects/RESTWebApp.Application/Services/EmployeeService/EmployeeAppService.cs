using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Data.Databases;
using RESTWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.EmployeeService
{
    public class EmployeeAppService : IEmployeeAppService, IDisposable
    {
        private readonly HumanResourcesContext _databaseContext;
        private IMapper? _mapper;

        public EmployeeAppService(HumanResourcesContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateEmployeeDto model)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var employee = _mapper.Map<MstEmployee>(model);
                        _databaseContext.Employees.Add(employee);
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
                return await Task.Run(() => (false, $"Error create employee: {outerEx.Message}"));
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
                        var employee = await _databaseContext.Employees.SingleOrDefaultAsync(w => w.Id == id);

                        if (employee != null)
                        {
                            _databaseContext.Employees.Remove(employee);
                            await _databaseContext.SaveChangesAsync();

                            await transaction.CommitAsync();
                            return await Task.Run(() => (true, "Employee removed successfully"));
                        }
                        else
                        {
                            return await Task.Run(() => (false, "Employee not found"));
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, $"Error removing employee: {ex.Message}"));
                    }
                }
            }
            catch (Exception outerEx)
            {
                return await Task.Run(() => (false, $"Error: {outerEx.Message}"));
            }
        }

        public async Task<PagedResult<EmployeeListDto>> GetAllEmployees(PageInfo pageinfo)
        {
            var employeesQuery = from employees in _databaseContext.Employees
                                 select new EmployeeListDto
                                 {
                                     Id = employees.Id,
                                     Code = employees.Code,
                                     Name = employees.Name,
                                     Age = employees.Age
                                 };

            var pagedResult = new PagedResult<EmployeeListDto>
            {
                Data = await employeesQuery
                            .OrderBy(w => w.Code)
                            .Skip(pageinfo.Skip)
                            .Take(pageinfo.PageSize)
                            .ToListAsync(),
                Total = await _databaseContext.Employees.CountAsync()
            };

            return await Task.Run(() => pagedResult);
        }

        public async Task<List<EmployeeListDto>> GetAllEmployees()
        {
            var employeesQuery = from employees in _databaseContext.Employees
                                 select new EmployeeListDto
                                 {
                                     Id = employees.Id,
                                     Code = employees.Code,
                                     Name = employees.Name,
                                     Age = employees.Age
                                 };
                       
            return await Task.Run(() => employeesQuery.ToListAsync());
        }

        public async Task<EmployeeListDto> GetEmployeeByCode(string code)
        {
            var employee = await _databaseContext.Employees
                .FirstOrDefaultAsync(w => w.Code == code);

            var employeeDto = _mapper.Map<EmployeeListDto>(employee);
            return await Task.Run(() => employeeDto);
        }

        public async Task<PagedResult<EmployeeListDto>> SearchEmployee(string searchString, PageInfo pageinfo)
        {
            var employeesQuery = from employee in _databaseContext.Employees
                                 select employee;

            if (!String.IsNullOrEmpty(searchString))
            {
                employeesQuery = employeesQuery.Where(s => s.Name.Contains(searchString) || s.Code.Contains(searchString));
            }

            var pagedResult = new PagedResult<EmployeeListDto>
            {
                Data = await employeesQuery
                            .OrderBy(w => w.Code)
                            .Skip(pageinfo.Skip)
                            .Take(pageinfo.PageSize)
                            .Select(employees => new EmployeeListDto
                            {
                                Id = employees.Id,
                                Code = employees.Code,
                                Name = employees.Name,
                                Age = employees.Age
                            })
                            .ToListAsync(),
                Total = await _databaseContext.Employees.CountAsync()
            };

            return await Task.Run(() => pagedResult);
        }

        public async Task<(bool, string)> Update(UpdateEmployeeDto model)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var employee = _mapper.Map<MstEmployee>(model);

                        _databaseContext.Employees.Update(employee);
                        await _databaseContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return await Task.Run(() => (true, "Success"));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return await Task.Run(() => (false, $"Error updating employee: {ex.Message}"));
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
