using AutoMapper;
using Moq;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Services.EmployeeService;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Data.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.UnitTesting.UnitServices
{
    public class EmployeeServiceUnitTest : Mock<IEmployeeAppService>
    {
        [Fact]
        public EmployeeServiceUnitTest GetAllProduct()
        {
            PageInfo pageInfo = new PageInfo(1, 5);
            pageInfo.Page = 1;
            pageInfo.PageSize = 5;

            PagedResult<EmployeeListDto> pagedResult = new PagedResult<EmployeeListDto>();

            Setup(x => x.GetAllEmployees(pageInfo))
                 .ReturnsAsync(pagedResult);

            return this;
        }
    }
}
