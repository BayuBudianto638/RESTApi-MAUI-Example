using AutoMapper;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.LoginService.Dto;
using RESTWebApp.Data.Models;
using RESTWebApp.Example.Models;

namespace RESTWebApp.Example.ConfigProfile
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();
        }
    }
}
