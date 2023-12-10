using AutoMapper;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.LoginService.Dto;
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

            CreateMap<MstEmployee, EmployeeListDto>();
            CreateMap<EmployeeListDto, MstEmployee>();

            CreateMap<MstEmployee, UpdateEmployeeDto>();
            CreateMap<UpdateEmployeeDto, MstEmployee>();

            CreateMap<MstUser, CreateUserDto>();
            CreateMap<CreateUserDto, MstUser>();

            CreateMap<MstUser, UpdateUserDto>();
            CreateMap<UpdateUserDto, MstUser>();

            CreateMap<MstUser, UserDto>();
            CreateMap<UserDto, MstUser>();
        }
    }
}
