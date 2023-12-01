using AutoMapper;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.ConfigProfile
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<MstEmployee, CreateEmployeeDto>();
            CreateMap<CreateEmployeeDto, MstEmployee>();
        }
    }
}
