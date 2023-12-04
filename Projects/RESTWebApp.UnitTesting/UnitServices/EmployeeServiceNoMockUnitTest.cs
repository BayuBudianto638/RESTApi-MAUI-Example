using Microsoft.Extensions.DependencyInjection;
using RESTWebApp.Application.Services.EmployeeService;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.UnitTesting.UnitServices
{
    public class EmployeeServiceNoMockUnitTest : IClassFixture<Startup>
    {
        private ServiceProvider _serviceProvider;

        public EmployeeServiceNoMockUnitTest(Startup fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void CreateEmployee()
        {
            var service = _serviceProvider.GetService<IEmployeeAppService>();

            CreateEmployeeDto createEmployeeDto = new CreateEmployeeDto();
            createEmployeeDto.Code = "PRD-001";
            createEmployeeDto.Name = "TEST";
            createEmployeeDto.Age = 1000;

            using var result = service.Create(createEmployeeDto);

            Assert.NotNull(result);
        }
    }
}
