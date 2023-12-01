using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Services.EmployeeService
{
    public interface IEmployeeAppService
    {
        Task<(bool, string)> Create(CreateEmployeeDto model);
        Task<(bool, string)> Update(UpdateEmployeeDto model);
        Task<(bool, string)> Delete(int id);
        Task<PagedResult<EmployeeListDto>> GetAllEmployees(PageInfo pageinfo);
        Task<UpdateEmployeeDto> GetEmployeeByCode(string code);
        Task<PagedResult<EmployeeListDto>> SearchEmployee(string searchString, PageInfo pageinfo);
    }
}
